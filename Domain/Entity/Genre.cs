namespace Domain.Entity;

/// <summary>
/// Domain Entity: Genre (Reference Data)
/// </summary>
/// <remarks>
/// Lookup table for game genres (e.g., RPG, FPS, Strategy)
/// </remarks>
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
