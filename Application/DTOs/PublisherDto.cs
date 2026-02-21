namespace Application.DTOs;

/// <summary>
/// Publisher Data Transfer Object
/// </summary>
public record PublisherDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
