using Application.DTOs;
using Application.Services;

namespace API.Endpoints;

/// <summary>
/// Genre REST API Endpoints
/// </summary>
public static class GenreEndpoints
{
    public static void MapGenreEndpoints(this WebApplication app)
    {
        // GET /genres - Retrieve all genres
        app.MapGet("/genres", async (IGenreService svc) =>
        {
            var genres = await svc.GetAllAsync();
            return Results.Ok(genres);
        })
        .WithName("GetAllGenres");

        // POST /genres - Create a new genre
        app.MapPost("/genres", async (AddGenreRequest input, IGenreService svc) =>
        {
            var id = await svc.AddAsync(input);
            return Results.Created($"/genres/{id}", new { id });
        })
        .WithName("AddGenre");
    }
}
