using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

/// <summary>
/// Design-time factory for creating VideoGamesDbContext instances
/// </summary>
/// <remarks>
/// This factory is required for EF Core design-time tools (migrations, scaffolding).
/// IDesignTimeDbContextFactory allows the tools to create a DbContext instance
/// when the application isn't running, such as when running:
/// - dotnet ef migrations add
/// - dotnet ef database update
/// - dotnet ef dbcontext scaffold
/// 
/// The factory reads configuration from appsettings.json in the Infrastructure project
/// to obtain the connection string at design time.
/// </remarks>
public class VideoGamesDbContextFactory : IDesignTimeDbContextFactory<VideoGamesDbContext>
{
    /// <summary>
    /// Creates a new instance of VideoGamesDbContext for design-time operations
    /// </summary>
    /// <param name="args">Command-line arguments (not used)</param>
    /// <returns>Configured VideoGamesDbContext instance</returns>
    public VideoGamesDbContext CreateDbContext(string[] args)
    {
        // Build configuration from appsettings.json in the current directory
        // This allows EF Core tools to access connection strings without running the app
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Configure DbContext with SQL Server using the connection string from config
        var options = new DbContextOptionsBuilder<VideoGamesDbContext>()
            .UseSqlServer(config.GetConnectionString("Default"))
            .Options;

        return new VideoGamesDbContext(options);
    }
}
