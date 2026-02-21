using Application.DTOs;
using Application.Services;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

/// <summary>
/// Game Service - Implements business logic for game management
/// </summary>
/// <remarks>
/// Design pattern: Service Layer (mediates between API and data access)
/// - Encapsulates data access logic
/// - Maps between domain entities and DTOs
/// - Uses EF Core for database operations
/// - Primary constructor syntax (C# 12) for cleaner DI
/// </remarks>
public class GameService(VideoGamesDbContext db) : IGameService
{
    private readonly VideoGamesDbContext _db = db;

    /// <summary>
    /// Creates a new game in the database
    /// </summary>
    /// <returns>The ID of the newly created game</returns>
    public async Task<int> AddAsync(AddGameRequest request)
    {
        // Map DTO to domain entity
        var entity = new Game
        {
            Title = request.Title,
            Description = request.Description,
            ReleaseDate = request.ReleaseDate,
            PublisherId = request.PublisherId,
            DeveloperId = request.DeveloperId
        };

        _db.Games.Add(entity);
        await _db.SaveChangesAsync();

        // Return the database-generated ID
        return entity.Id;
    }

    /// <summary>
    /// Deletes a game by ID (idempotent operation)
    /// </summary>
    public async Task DeleteAsync(int id)
    {
        var game = await _db.Games.FindAsync(id);

        // Gracefully handle non-existent records (idempotent DELETE)
        if (game is null) return;

        _db.Games.Remove(game);
        await _db.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves all games with related data (Developer, Publisher)
    /// </summary>
    /// <remarks>
    /// Uses EF Core's Include for eager loading to avoid N+1 query problem
    /// Projects to DTO to avoid exposing domain entities to API layer
    /// </remarks>
    public async Task<IEnumerable<GameDto>> GetAllAsync()
    {
        return await _db.Games
            .Include(g => g.Developer)   // Eager load related Developer
            .Include(g => g.Publisher)   // Eager load related Publisher
            .Select(g => new GameDto     // Project to DTO (prevents over-fetching)
            {
                Id = g.Id,
                Title = g.Title,
                Description = g.Description,
                ReleaseDate = g.ReleaseDate,
                PublisherId = g.PublisherId,
                DeveloperId = g.DeveloperId
            })
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a single game by ID with related data
    /// </summary>
    /// <returns>GameDto if found, null otherwise</returns>
    public async Task<GameDto?> GetByIdAsync(int id)
    {
        var g = await _db.Games
            .Include(x => x.Developer)
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (g is null) return null;

        // Manual mapping (could use AutoMapper in larger projects)
        return new GameDto
        {
            Id = g.Id,
            Title = g.Title,
            Description = g.Description,
            ReleaseDate = g.ReleaseDate,
            PublisherId = g.PublisherId,
            DeveloperId = g.DeveloperId
        };
    }

    /// <summary>
    /// Updates an existing game (idempotent operation)
    /// </summary>
    public async Task UpdateAsync(int id, UpdateGameRequest request)
    {
        var game = await _db.Games.FindAsync(id);

        // Gracefully handle non-existent records (idempotent PUT)
        if (game is null) return;

        // Update entity properties
        game.Title = request.Title;
        game.Description = request.Description;
        game.ReleaseDate = request.ReleaseDate;
        game.PublisherId = request.PublisherId;
        game.DeveloperId = request.DeveloperId;

        // EF Core tracks changes automatically
        await _db.SaveChangesAsync();
    }
}
