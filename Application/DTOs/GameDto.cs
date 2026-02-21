namespace Application.DTOs;

public record GameDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public int? PublisherId { get; set; }
    public int? DeveloperId { get; set; }
}
