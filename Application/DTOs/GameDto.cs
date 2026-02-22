namespace Application.DTOs;

/// <summary>
/// Data Transfer Object for Game entity
/// </summary>
/// <remarks>
/// Purpose: Decouple API responses from database entities
/// - Prevents over-posting attacks
/// - Controls what data is exposed to clients
/// - Allows different representations for different operations
/// - Record type provides immutability and value equality
/// </remarks>
public record GameDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? ReleaseDate { get; set; }

    // Related objects included for richer API responses
    public GenreDto? Genre { get; set; }
    public PublisherDto? Publisher { get; set; }
    public DeveloperDto? Developer { get; set; }
}
