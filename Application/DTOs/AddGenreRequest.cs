namespace Application.DTOs;

/// <summary>
/// Request DTO for creating a new genre
/// </summary>
public record AddGenreRequest
{
    public string Name { get; set; } = null!;
}
