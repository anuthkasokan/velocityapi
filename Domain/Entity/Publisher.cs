namespace Domain.Entity;

/// <summary>
/// Domain Entity: Publisher
/// </summary>
/// <remarks>
/// One-to-many relationship: A publisher can publish multiple games
/// </remarks>
public class Publisher
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Navigation property: Collection of published games
    public ICollection<Game> Games { get; set; } = [];
}
