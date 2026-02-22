namespace Application.DTOs;

/// <summary>
/// Request DTO for creating a new game
/// </summary>
/// <remarks>
/// Separate from GameDto to:
/// - Exclude ID (database-generated)
/// - Apply different validation rules for creation vs retrieval
/// - Follow CQRS pattern separation (Command vs Query)
/// </remarks>
public record AddGameRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? GenreId { get; set; }
    public int? PublisherId { get; set; }
    public int? DeveloperId { get; set; }
}
