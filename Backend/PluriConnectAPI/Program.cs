using Microsoft.EntityFrameworkCore;
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

// Ensure DB
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    ctx.Database.EnsureCreated(); // rápido para hackatón
}

app.MapGet("/", () => "Backend funcionando con EF Core + SQLite");

// Map CRUD async (DI inyecta `GenericService<T>` en cada endpoint)
app.MapCrudAsync<Child>("children");
app.MapCrudAsync<Goal>("goals");
app.MapCrudAsync<Activity>("activities");
app.MapCrudAsync<Progress>("progress");
app.MapCrudAsync<GoalActivity>("goalActivities");
app.MapCrudAsync<Comment>("comments");

app.Run();
