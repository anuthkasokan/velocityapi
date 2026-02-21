using Application.DTOs;
using Application.Services;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class GameService(VideoGamesDbContext db) : IGameService
{
    private readonly VideoGamesDbContext _db = db;

    public async Task<int> AddAsync(AddGameRequest request)
    {
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
        return entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var game = await _db.Games.FindAsync(id);
        if (game is null) return;
        _db.Games.Remove(game);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<GameDto>> GetAllAsync()
    {
        return await _db.Games
            .Include(g => g.Developer)
            .Include(g => g.Publisher)
            .Select(g => new GameDto
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

    public async Task<GameDto?> GetByIdAsync(int id)
    {
        var g = await _db.Games
            .Include(x => x.Developer)
            .Include(x => x.Publisher)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (g is null) return null;
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

    public async Task UpdateAsync(int id, UpdateGameRequest request)
    {
        var game = await _db.Games.FindAsync(id);
        if (game is null) return;
        game.Title = request.Title;
        game.Description = request.Description;
        game.ReleaseDate = request.ReleaseDate;
        game.PublisherId = request.PublisherId;
        game.DeveloperId = request.DeveloperId;
        await _db.SaveChangesAsync();
    }
}
