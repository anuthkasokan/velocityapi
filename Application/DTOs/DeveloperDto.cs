namespace Application.DTOs;

/// <summary>
/// Developer Data Transfer Object
/// </summary>
public record DeveloperDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
