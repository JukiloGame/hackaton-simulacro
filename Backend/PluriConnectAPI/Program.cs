using Microsoft.EntityFrameworkCore;
using PluriConnectAPI.Data;
using PluriConnectAPI.Models;
using PluriConnectAPI.Services;
using PluriConnectAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// DbContext (SQLite)
var dbPath = Path.Combine("Data", "pluriconnect.db");
Directory.CreateDirectory("Data"); // asegurar carpeta
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// Generic services (scoped)
builder.Services.AddScoped(typeof(GenericService<>));

var app = builder.Build();
app.UseCors("FrontendPolicy");

// Ensure DB + Seed
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.EnsureCreated(); // rápido para hackatón
    await DbSeeder.SeedInitialAsync(ctx, "Data");
}

// Resolver servicios para MapCrud
var childService = app.Services.CreateScope().ServiceProvider.GetRequiredService<GenericService<Child>>();
var goalService = app.Services.CreateScope().ServiceProvider.GetRequiredService<GenericService<Goal>>();
var activityService = app.Services.CreateScope().ServiceProvider.GetRequiredService<GenericService<Activity>>();
var progressService = app.Services.CreateScope().ServiceProvider.GetRequiredService<GenericService<Progress>>();
var goalActivityService = app.Services.CreateScope().ServiceProvider.GetRequiredService<GenericService<GoalActivities>>();
var commentService = app.Services.CreateScope().ServiceProvider.GetRequiredService<GenericService<Comment>>();

app.MapGet("/", () => "Backend funcionando con EF Core + SQLite");

// Map CRUD async
app.MapCrudAsync("children", childService);
app.MapCrudAsync("goals", goalService);
app.MapCrudAsync("activities", activityService);
app.MapCrudAsync("progress", progressService);
app.MapCrudAsync("goalActivities", goalActivityService);
app.MapCrudAsync("comments", commentService);

app.Run();
