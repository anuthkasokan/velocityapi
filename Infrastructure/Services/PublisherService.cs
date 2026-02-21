using Application.DTOs;
using Application.Services;
using Domain.Entity;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

/// <summary>
/// Publisher Service - Implements business logic for publisher management
/// </summary>
public class PublisherService(VideoGamesDbContext db) : IPublisherService
{
    private readonly VideoGamesDbContext _db = db;

    /// <summary>
    /// Creates a new publisher in the database
    /// </summary>
    /// <returns>The ID of the newly created publisher</returns>
    public async Task<int> AddAsync(AddPublisherRequest request)
    {
        var entity = new Publisher
        {
            Name = request.Name
        };

        _db.Publishers.Add(entity);
        await _db.SaveChangesAsync();

        return entity.Id;
    }

    /// <summary>
    /// Retrieves all publishers
    /// </summary>
    public async Task<IEnumerable<PublisherDto>> GetAllAsync()
    {
        return await _db.Publishers
            .Select(p => new PublisherDto
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToListAsync();
    }
}
