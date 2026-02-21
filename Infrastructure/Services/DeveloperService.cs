using Application.DTOs;
using Application.Services;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

/// <summary>
/// Developer Service - Implements business logic for developer management
/// </summary>
public class DeveloperService(VideoGamesDbContext db) : IDeveloperService
{
    private readonly VideoGamesDbContext _db = db;

    /// <summary>
    /// Creates a new developer in the database
    /// </summary>
    /// <returns>The ID of the newly created developer</returns>
    public async Task<int> AddAsync(AddDeveloperRequest request)
    {
        var entity = new Developer
        {
            Name = request.Name
        };

        _db.Developers.Add(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }

    /// <summary>
    /// Retrieves all developers
    /// </summary>
    public async Task<IEnumerable<DeveloperDto>> GetAllAsync()
    {
        return await _db.Developers
            .Select(d => new DeveloperDto
            {
                Id = d.Id,
                Name = d.Name
            })
            .ToListAsync();
    }
}
