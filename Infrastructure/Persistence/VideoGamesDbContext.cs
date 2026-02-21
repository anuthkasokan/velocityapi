using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// EF Core DbContext for Video Games Catalogue
/// </summary>
/// <remarks>
/// Architecture: Infrastructure layer (data access)
/// - Configures database schema using Fluent API
/// - Primary constructor syntax for DbContextOptions injection
/// - Code-first approach with migrations
/// - Connection string managed in appsettings.json
/// </remarks>
public class VideoGamesDbContext(DbContextOptions<VideoGamesDbContext> options) : DbContext(options)
{
    // DbSets represent database tables
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<Developer> Developers { get; set; } = null!;

    /// <summary>
    /// Configure entity mappings and database schema using Fluent API
    /// </summary>
    /// <remarks>
    /// Fluent API provides more control than Data Annotations:
    /// - Configure column types, lengths, constraints
    /// - Define relationships and navigation properties
    /// - Set up indexes, keys, and database-specific features
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Game entity configuration
        modelBuilder.Entity<Game>(b =>
        {
            // Required field with max length (prevents SQL truncation errors)
            b.Property(g => g.Title).IsRequired().HasMaxLength(200);
        });

        // Configure entity constraints for reference data tables
        modelBuilder.Entity<Genre>(b => b.Property(g => g.Name).IsRequired().HasMaxLength(100));
        modelBuilder.Entity<Platform>(b => b.Property(p => p.Name).IsRequired().HasMaxLength(100));
        modelBuilder.Entity<Publisher>(b => b.Property(p => p.Name).IsRequired().HasMaxLength(200));
        modelBuilder.Entity<Developer>(b => b.Property(d => d.Name).IsRequired().HasMaxLength(200));
    }
}
