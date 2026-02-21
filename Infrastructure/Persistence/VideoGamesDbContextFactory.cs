using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class VideoGamesDbContextFactory : IDesignTimeDbContextFactory<VideoGamesDbContext>
{
    public VideoGamesDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var options = new DbContextOptionsBuilder<VideoGamesDbContext>()
            .UseSqlServer(config.GetConnectionString("Default"))
            .Options;

        return new VideoGamesDbContext(options);
    }
}
