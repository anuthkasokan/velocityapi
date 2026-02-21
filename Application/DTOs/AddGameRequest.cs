namespace Application.DTOs;

public record AddGameRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? PublisherId { get; set; }
    public int? DeveloperId { get; set; }
}
