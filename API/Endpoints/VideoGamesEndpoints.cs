using Application.DTOs;
using Application.Services;

namespace API.Endpoints;

/// <summary>
/// Video Games REST API Endpoints
/// Implements RESTful CRUD operations using Minimal APIs (.NET 6+)
/// </summary>
/// <remarks>
/// Design decisions:
/// - Uses extension method pattern for clean organization
/// - Parameter injection for dependencies (IGameService)
/// - Follows REST conventions (GET, POST, PUT, DELETE)
/// - Returns appropriate HTTP status codes
/// </remarks>
public static class VideoGamesEndpoints
{
    /// <summary>
    /// Maps all video game endpoints to the application
    /// </summary>
    public static void MapVideoGamesEndpoints(this WebApplication app)
    {
        // GET /games - Retrieve all games
        // Returns: 200 OK with array of GameDto
        app.MapGet("/games", async (IGameService svc) =>
        {
            var games = await svc.GetAllAsync();
            return Results.Ok(games);
        })
        .WithName("GetAllGames");

        // GET /games/{id} - Retrieve a specific game by ID
        // Returns: 200 OK with GameDto | 404 Not Found
        app.MapGet("/games/{id:int}", async (int id, IGameService svc) =>
        {
            var game = await svc.GetByIdAsync(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        })
        .WithName("GetGameById");

        // POST /games - Create a new game
        // Returns: 201 Created with Location header and created resource ID
        app.MapPost("/games", async (AddGameRequest input, IGameService svc) =>
        {
            var id = await svc.AddAsync(input);
            return Results.Created($"/games/{id}", new { id });
        })
        .WithName("AddGame");

        // PUT /games/{id} - Update an existing game
        // Returns: 204 No Content (idempotent operation)
        app.MapPut("/games/{id:int}", async (int id, UpdateGameRequest input, IGameService svc) =>
        {
            await svc.UpdateAsync(id, input);
            return Results.NoContent();
        })
        .WithName("UpdateGame");

        // DELETE /games/{id} - Delete a game
        // Returns: 204 No Content (idempotent operation)
        app.MapDelete("/games/{id:int}", async (int id, IGameService svc) =>
        {
            await svc.DeleteAsync(id);
            return Results.NoContent();
        })
        .WithName("DeleteGame");
    }
}
