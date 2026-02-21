using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class VideoGamesDbContext(DbContextOptions<VideoGamesDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Platform> Platforms { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Publisher> Publishers { get; set; } = null!;
    public DbSet<Developer> Developers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(b =>
        {
            b.Property(g => g.Title).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Genre>(b => b.Property(g => g.Name).IsRequired().HasMaxLength(100));
        modelBuilder.Entity<Platform>(b => b.Property(p => p.Name).IsRequired().HasMaxLength(100));
        modelBuilder.Entity<Publisher>(b => b.Property(p => p.Name).IsRequired().HasMaxLength(200));
        modelBuilder.Entity<Developer>(b => b.Property(d => d.Name).IsRequired().HasMaxLength(200));
    }
}
