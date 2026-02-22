using Application.DTOs;
using Domain.Entity;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Services;

[TestFixture]
public class GameServiceTests
{
    private static VideoGamesDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<VideoGamesDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new VideoGamesDbContext(options);
    }

    [Test]
    public async Task AddAsync_ShouldAddGameAndReturnId()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var service = new GameService(context);
        var request = new AddGameRequest
        {
            Title = "The Legend of Zelda",
            Description = "Action-adventure game",
            ReleaseDate = new DateTime(1986, 2, 21),
            PublisherId = 1,
            DeveloperId = 1
        };

        // Act
        var gameId = await service.AddAsync(request);

        // Assert
        Assert.That(gameId, Is.GreaterThan(0));
        var game = await context.Games.FindAsync(gameId);
        Assert.That(game, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(game.Title, Is.EqualTo("The Legend of Zelda"));
            Assert.That(game.Description, Is.EqualTo("Action-adventure game"));
            Assert.That(game.ReleaseDate, Is.EqualTo(new DateTime(1986, 2, 21)));
        });

    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllGames()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var developer = new Developer { Id = 1, Name = "Nintendo" };
        var publisher = new Publisher { Id = 1, Name = "Nintendo" };
        context.Developers.Add(developer);
        context.Publishers.Add(publisher);
        context.Games.AddRange(
            new Game { Id = 1, Title = "Game 1", DeveloperId = 1, PublisherId = 1 },
            new Game { Id = 2, Title = "Game 2", DeveloperId = 1, PublisherId = 1 },
            new Game { Id = 3, Title = "Game 3", DeveloperId = 1, PublisherId = 1 }
        );
        await context.SaveChangesAsync();

        var service = new GameService(context);

        // Act
        var games = await service.GetAllAsync();

        // Assert
        Assert.That(games.Count(), Is.EqualTo(3));
    }

    [Test]
    public async Task GetByIdAsync_ExistingGame_ShouldReturnGame()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var developer = new Developer { Id = 1, Name = "Nintendo" };
        var publisher = new Publisher { Id = 1, Name = "Nintendo" };
        context.Developers.Add(developer);
        context.Publishers.Add(publisher);
        var game = new Game
        {
            Id = 1,
            Title = "Super Mario Bros",
            Description = "Platform game",
            ReleaseDate = new DateTime(1985, 9, 13),
            DeveloperId = 1,
            PublisherId = 1
        };
        context.Games.Add(game);
        await context.SaveChangesAsync();

        var service = new GameService(context);

        // Act
        var result = await service.GetByIdAsync(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Title, Is.EqualTo("Super Mario Bros"));
            Assert.That(result.Description, Is.EqualTo("Platform game"));
            Assert.That(result.ReleaseDate, Is.EqualTo(new DateTime(1985, 9, 13)));
        });

    }

    [Test]
    public async Task GetByIdAsync_NonExistingGame_ShouldReturnNull()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var service = new GameService(context);

        // Act
        var result = await service.GetByIdAsync(999);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task UpdateAsync_ExistingGame_ShouldUpdateGame()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var game = new Game
        {
            Id = 1,
            Title = "Original Title",
            Description = "Original Description",
            ReleaseDate = new DateTime(2020, 1, 1),
            PublisherId = 1,
            DeveloperId = 1
        };
        context.Games.Add(game);
        await context.SaveChangesAsync();

        var service = new GameService(context);
        var updateRequest = new UpdateGameRequest
        {
            Title = "Updated Title",
            Description = "Updated Description",
            ReleaseDate = new DateTime(2021, 5, 15),
            PublisherId = 2,
            DeveloperId = 2
        };

        // Act
        await service.UpdateAsync(1, updateRequest);

        // Assert
        var updatedGame = await context.Games.FindAsync(1);
        Assert.That(updatedGame, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(updatedGame.Title, Is.EqualTo("Updated Title"));
            Assert.That(updatedGame.Description, Is.EqualTo("Updated Description"));
            Assert.That(updatedGame.ReleaseDate, Is.EqualTo(new DateTime(2021, 5, 15)));
            Assert.That(updatedGame.PublisherId, Is.EqualTo(2));
            Assert.That(updatedGame.DeveloperId, Is.EqualTo(2));
        });

    }

    [Test]
    public async Task UpdateAsync_NonExistingGame_ShouldNotThrowException()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var service = new GameService(context);
        var updateRequest = new UpdateGameRequest
        {
            Title = "Non-existing Game",
            Description = "This game doesn't exist"
        };

        // Act & Assert
        Assert.DoesNotThrowAsync(async () => await service.UpdateAsync(999, updateRequest));
    }

    [Test]
    public async Task DeleteAsync_ExistingGame_ShouldRemoveGame()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var game = new Game { Id = 1, Title = "To Be Deleted" };
        context.Games.Add(game);
        await context.SaveChangesAsync();

        var service = new GameService(context);

        // Act
        await service.DeleteAsync(1);

        // Assert
        var deletedGame = await context.Games.FindAsync(1);
        Assert.That(deletedGame, Is.Null);
    }

    [Test]
    public async Task DeleteAsync_NonExistingGame_ShouldNotThrowException()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var service = new GameService(context);

        // Act & Assert
        Assert.DoesNotThrowAsync(async () => await service.DeleteAsync(999));
    }

    [Test]
    public async Task GetAllAsync_WithIncludedRelations_ShouldLoadDeveloperAndPublisher()
    {
        // Arrange
        await using var context = GetInMemoryDbContext();
        var developer = new Developer { Id = 1, Name = "Nintendo EPD" };
        var publisher = new Publisher { Id = 1, Name = "Nintendo Publishing" };
        var genre = new Genre { Id = 1, Name = "Action" };
        context.Developers.Add(developer);
        context.Publishers.Add(publisher);
        context.Genres.Add(genre);

        var game = new Game
        {
            Id = 1,
            Title = "Metroid Prime",
            DeveloperId = 1,
            PublisherId = 1,
            GenreId = 1
        };
        context.Games.Add(game);
        await context.SaveChangesAsync();

        var service = new GameService(context);

        // Act
        var games = await service.GetAllAsync();

        // Assert
        var gameDto = games.FirstOrDefault();
        Assert.That(gameDto, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(gameDto.Genre?.Id, Is.EqualTo(1));
            Assert.That(gameDto.Publisher?.Id, Is.EqualTo(1));
            Assert.That(gameDto.Developer?.Id, Is.EqualTo(1));
        });

    }
}
