using Application.DTOs;
using Application.Services;

namespace API.Endpoints;

public static class VideoGamesEndpoints
{
    public static void MapVideoGamesEndpoints(this WebApplication app)
    {
        app.MapGet("/games", async (IGameService svc) =>
        {
            var games = await svc.GetAllAsync();
            return Results.Ok(games);
        });

        app.MapGet("/games/{id:int}", async (int id, IGameService svc) =>
        {
            var game = await svc.GetByIdAsync(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        });

        app.MapPost("/games", async (AddGameRequest input, IGameService svc) =>
        {
            var id = await svc.AddAsync(input);
            return Results.Created($"/games/{id}", new { id });
        });

        app.MapPut("/games/{id:int}", async (int id, UpdateGameRequest input, IGameService svc) =>
        {
            await svc.UpdateAsync(id, input);
            return Results.NoContent();
        });

        app.MapDelete("/games/{id:int}", async (int id, IGameService svc) =>
        {
            await svc.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
