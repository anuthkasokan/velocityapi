namespace Application.DTOs;

/// <summary>
/// Request DTO for updating an existing game
/// </summary>
/// <remarks>
/// Separate from AddGameRequest to allow different validation/requirements
/// - PUT semantic: full resource update
/// - ID comes from route parameter, not body
/// </remarks>
public record UpdateGameRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? PublisherId { get; set; }
    public int? DeveloperId { get; set; }
}
