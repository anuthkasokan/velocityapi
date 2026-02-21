using API.Endpoints;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Register EF Core DbContext (SQL Server) and application services
builder.Services.AddDbContext<VideoGamesDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapVideoGamesEndpoints();

app.Run();
