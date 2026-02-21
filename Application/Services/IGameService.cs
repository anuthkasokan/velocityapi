using Application.DTOs;

namespace Application.Services;

public interface IGameService
{
    Task<IEnumerable<GameDto>> GetAllAsync();
    Task<GameDto?> GetByIdAsync(int id);
    Task<int> AddAsync(AddGameRequest request);
    Task UpdateAsync(int id, UpdateGameRequest request);
    Task DeleteAsync(int id);
}
