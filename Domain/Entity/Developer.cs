namespace Domain.Entity;

/// <summary>
/// Domain Entity: Developer (Game Studio)
/// </summary>
/// <remarks>
/// One-to-many relationship: A developer can create multiple games
/// Navigation property (Games) enables EF Core to manage the relationship
/// </remarks>
public class Developer
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    // Navigation property: Collection of games developed by this developer
    public ICollection<Game> Games { get; set; } = [];
}
