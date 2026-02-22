namespace Domain.Entity;

/// <summary>
/// Domain Entity: Game
/// </summary>
/// <remarks>
/// Architecture: Domain layer (core business entities)
/// - Represents a video game in the system
/// - EF Core conventions: Id property is primary key
/// - Navigation properties for relationships (Publisher, Developer)
/// - Nullable foreign keys allow games without publisher/developer
/// </remarks>
public class Game
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public int? GenreId { get; set; }
    public Genre? Genre { get; set; }

    public int? PublisherId { get; set; }
    public Publisher? Publisher { get; set; }

    public int? DeveloperId { get; set; }
    public Developer? Developer { get; set; }
}
