using Application.DTOs;

namespace Application.Services;

/// <summary>
/// Publisher Service Contract
/// </summary>
public interface IPublisherService
{
    /// <summary>Retrieves all publishers</summary>
    Task<IEnumerable<PublisherDto>> GetAllAsync();

    /// <summary>Creates a new publisher</summary>
    /// <returns>ID of the created publisher</returns>
    Task<int> AddAsync(AddPublisherRequest request);
}
