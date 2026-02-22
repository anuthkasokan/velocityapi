using Application.DTOs;

namespace Application.Services;

/// <summary>
/// Genre Service Contract
/// </summary>
public interface IGenreService
{
    /// <summary>Retrieves all genres</summary>
    Task<IEnumerable<GenreDto>> GetAllAsync();

    /// <summary>Creates a new genre</summary>
    /// <returns>ID of the created genre</returns>
    Task<int> AddAsync(AddGenreRequest request);
}
