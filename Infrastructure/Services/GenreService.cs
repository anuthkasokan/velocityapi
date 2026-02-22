using Application.DTOs;
using Application.Services;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

/// <summary>
/// Genre Service - Implements business logic for genre management
/// </summary>
public class GenreService(VideoGamesDbContext db) : IGenreService
{
    private readonly VideoGamesDbContext _db = db;

    public async Task<int> AddAsync(AddGenreRequest request)
    {
        var entity = new Genre
        {
            Name = request.Name
        };

        _db.Genres.Add(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<IEnumerable<GenreDto>> GetAllAsync()
    {
        return await _db.Genres
            .Select(g => new GenreDto { Id = g.Id, Name = g.Name })
            .ToListAsync();
    }
}
