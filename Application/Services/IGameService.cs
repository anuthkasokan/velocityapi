using Application.DTOs;

namespace Application.Services;

/// <summary>
/// Game Service Contract (Dependency Inversion Principle)
/// </summary>
/// <remarks>
/// Architecture: Application layer (business logic abstraction)
/// - Defines operations without implementation details
/// - Allows Infrastructure to implement data access
/// - Enables easy mocking for unit tests
/// - Follows Interface Segregation Principle
/// </remarks>
public interface IGameService
{
    /// <summary>Retrieves all games</summary>
    Task<IEnumerable<GameDto>> GetAllAsync();

    /// <summary>Retrieves a game by ID</summary>
    Task<GameDto?> GetByIdAsync(int id);

    /// <summary>Creates a new game</summary>
    /// <returns>ID of the created game</returns>
    Task<int> AddAsync(AddGameRequest request);

    /// <summary>Updates an existing game</summary>
    Task UpdateAsync(int id, UpdateGameRequest request);

    /// <summary>Deletes a game by ID</summary>
    Task DeleteAsync(int id);
}
