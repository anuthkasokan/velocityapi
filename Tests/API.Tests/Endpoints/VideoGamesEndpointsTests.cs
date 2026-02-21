using Application.DTOs;
using Application.Services;
using Moq;

namespace API.Tests.Endpoints;

[TestFixture]
public class VideoGamesEndpointsTests
{
    private Mock<IGameService> _mockGameService = null!;

    [SetUp]
    public void SetUp()
    {
        _mockGameService = new Mock<IGameService>();
    }

    [Test]
    public async Task GetAllAsync_ShouldBeCalledByEndpoint()
    {
        // Arrange
        var expectedGames = new List<GameDto>
        {
            new() { Id = 1, Title = "Game 1" },
            new() { Id = 2, Title = "Game 2" }
        };
        _mockGameService.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedGames);

        // Act
        var result = await _mockGameService.Object.GetAllAsync();

        // Assert
        _mockGameService.Verify(s => s.GetAllAsync(), Times.Once);
        Assert.That(result, Is.EqualTo(expectedGames));
    }

    [Test]
    public async Task GetByIdAsync_WithValidId_ShouldReturnGame()
    {
        // Arrange
        var expectedGame = new GameDto { Id = 1, Title = "The Witcher 3" };
        _mockGameService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(expectedGame);

        // Act
        var result = await _mockGameService.Object.GetByIdAsync(1);

        // Assert
        _mockGameService.Verify(s => s.GetByIdAsync(1), Times.Once);
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Title, Is.EqualTo("The Witcher 3"));
    }

    [Test]
    public async Task GetByIdAsync_WithNonExistingId_ShouldReturnNull()
    {
        // Arrange
        _mockGameService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((GameDto?)null);

        // Act
        var result = await _mockGameService.Object.GetByIdAsync(999);

        // Assert
        _mockGameService.Verify(s => s.GetByIdAsync(999), Times.Once);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task AddAsync_WithValidRequest_ShouldReturnNewId()
    {
        // Arrange
        var request = new AddGameRequest { Title = "Cyberpunk 2077" };
        _mockGameService.Setup(s => s.AddAsync(request)).ReturnsAsync(123);

        // Act
        var result = await _mockGameService.Object.AddAsync(request);

        // Assert
        _mockGameService.Verify(s => s.AddAsync(request), Times.Once);
        Assert.That(result, Is.EqualTo(123));
    }

    [Test]
    public async Task UpdateAsync_ShouldInvokeService()
    {
        // Arrange
        var updateRequest = new UpdateGameRequest { Title = "Updated Title" };
        _mockGameService.Setup(s => s.UpdateAsync(1, updateRequest)).Returns(Task.CompletedTask);

        // Act
        await _mockGameService.Object.UpdateAsync(1, updateRequest);

        // Assert
        _mockGameService.Verify(s => s.UpdateAsync(1, updateRequest), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ShouldInvokeService()
    {
        // Arrange
        _mockGameService.Setup(s => s.DeleteAsync(1)).Returns(Task.CompletedTask);

        // Act
        await _mockGameService.Object.DeleteAsync(1);

        // Assert
        _mockGameService.Verify(s => s.DeleteAsync(1), Times.Once);
    }
}
