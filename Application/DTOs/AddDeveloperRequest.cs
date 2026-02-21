namespace Application.DTOs;

/// <summary>
/// Request DTO for creating a new developer
/// </summary>
public record AddDeveloperRequest
{
    public string Name { get; set; } = null!;
}
