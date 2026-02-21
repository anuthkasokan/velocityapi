// ===================================================================
// Video Games Catalogue API - Entry Point
// ===================================================================
// This API demonstrates Clean Architecture principles with:
// - Minimal APIs (lightweight alternative to controllers)
// - Dependency Injection for loose coupling
// - EF Core with SQL Server for data persistence
// - Code-first migrations for database schema management
// ===================================================================

using API.Endpoints;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===================================================================
// Service Registration (Dependency Injection Container)
// ===================================================================

// Enable OpenAPI/Swagger for API documentation and testing
builder.Services.AddOpenApi();

// Register EF Core DbContext with SQL Server provider
builder.Services.AddDbContext<VideoGamesDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddScoped<IDeveloperService, DeveloperService>();

var app = builder.Build();

// ===================================================================
// HTTP Request Pipeline Configuration
// ===================================================================

// Enable OpenAPI/Swagger UI in development for interactive API testing
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirect HTTP requests to HTTPS for security
app.UseHttpsRedirection();

// Map minimal API endpoints using extension method pattern
// - Keeps Program.cs clean and maintainable
// - Endpoints organized by feature/resource
app.MapVideoGamesEndpoints();
app.MapPublisherEndpoints();
app.MapDeveloperEndpoints();

app.Run();
