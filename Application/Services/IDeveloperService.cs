using Application.DTOs;

namespace Application.Services;

/// <summary>
/// Developer Service Contract
/// </summary>
public interface IDeveloperService
{
    /// <summary>Retrieves all developers</summary>
    Task<IEnumerable<DeveloperDto>> GetAllAsync();

    /// <summary>Creates a new developer</summary>
    /// <returns>ID of the created developer</returns>
    Task<int> AddAsync(AddDeveloperRequest request);
}
