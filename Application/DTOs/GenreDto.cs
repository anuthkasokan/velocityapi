namespace Application.DTOs;

/// <summary>
/// Genre DTO for API responses
/// </summary>
public record GenreDto
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
}
