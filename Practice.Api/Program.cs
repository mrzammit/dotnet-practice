using Practice.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<ItemRepository>();

var app = builder.Build();

app.MapControllers();

app.MapGet("/api/ping", () => "pong");

app.Run();
