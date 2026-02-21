namespace Application.DTOs;

/// <summary>
/// Request DTO for creating a new publisher
/// </summary>
public record AddPublisherRequest
{
    public string Name { get; set; } = null!;
}
