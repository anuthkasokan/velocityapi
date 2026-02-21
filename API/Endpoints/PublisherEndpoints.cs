using Application.DTOs;
using Application.Services;

namespace API.Endpoints;

/// <summary>
/// Publisher REST API Endpoints
/// </summary>
public static class PublisherEndpoints
{
    /// <summary>
    /// Maps all publisher endpoints to the application
    /// </summary>
    public static void MapPublisherEndpoints(this WebApplication app)
    {
        // GET /publishers - Retrieve all publishers
        app.MapGet("/publishers", async (IPublisherService svc) =>
        {
            var publishers = await svc.GetAllAsync();
            return Results.Ok(publishers);
        })
        .WithName("GetAllPublishers");

        // POST /publishers - Add a new publisher
        app.MapPost("/publishers", async (AddPublisherRequest input, IPublisherService svc) =>
        {
            var id = await svc.AddAsync(input);
            return Results.Created($"/publishers/{id}", new { id });
        })
        .WithName("AddPublisher");
    }
}
