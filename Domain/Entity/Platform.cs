namespace Domain.Entity;

/// <summary>
/// Domain Entity: Platform (Reference Data)
/// </summary>
/// <remarks>
/// Lookup table for gaming platforms (e.g., PC, PlayStation, Xbox)
/// </remarks>
public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
