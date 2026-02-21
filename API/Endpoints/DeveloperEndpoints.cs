using Application.DTOs;
using Application.Services;

namespace API.Endpoints;

/// <summary>
/// Developer REST API Endpoints
/// </summary>
public static class DeveloperEndpoints
{
    /// <summary>
    /// Maps all developer endpoints to the application
    /// </summary>
    public static void MapDeveloperEndpoints(this WebApplication app)
    {
        // GET /developers - Retrieve all developers
        app.MapGet("/developers", async (IDeveloperService svc) =>
        {
            var developers = await svc.GetAllAsync();
            return Results.Ok(developers);
        })
        .WithName("GetAllDevelopers");

        // POST /developers - Add a new developer
        app.MapPost("/developers", async (AddDeveloperRequest input, IDeveloperService svc) =>
        {
            var id = await svc.AddAsync(input);
            return Results.Created($"/developers/{id}", new { id });
        })
        .WithName("AddDeveloper");
    }
}
