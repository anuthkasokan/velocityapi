namespace Domain.Entity;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public int? PublisherId { get; set; }
    public Publisher? Publisher { get; set; }

    public int? DeveloperId { get; set; }
    public Developer? Developer { get; set; }
}
