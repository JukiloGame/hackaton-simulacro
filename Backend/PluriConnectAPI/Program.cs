// using LiteDB;

// var builder = WebApplication.CreateBuilder(args);
// var app = builder.Build();

// var db = new LiteDatabase(@"Data\pluriconnect.db");
// var children = db.GetCollection<Child>("children");

// app.MapGet("/", () => "Backend funcionando!");
// app.MapGet("/children", () => children.FindAll());

// app.Run();

// public class Child
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public DateTime Dob { get; set; }
// }



using LiteDB;
using PluriConnectAPI.Models;
using PluriConnectAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("FrontendPolicy", policy =>
	{
    policy
        .WithOrigins("http://localhost:5173")
        .AllowAnyMethod()
        .AllowAnyHeader();
	});
});

var app = builder.Build();

app.UseCors("FrontendPolicy");

var db = new LiteDatabase(@"Data\pluriconnect.db");

// Inicializar datos de prueba si la base de datos está vacía
DbSeeder.Seed<Child>(db, "Data/sampleChildren.json");
DbSeeder.Seed<Goal>(db, "Data/sampleGoals.json");
DbSeeder.Seed<Activity>(db, "Data/sampleActivities.json");
DbSeeder.Seed<Progress>(db, "Data/sampleProgress.json");
DbSeeder.Seed<GoalActivities>(db, "Data/sampleGoalActivities.json");

// Generic Services para cada modelo
var childService = new GenericService<Child>(db);
var goalService = new GenericService<Goal>(db);
var activityService = new GenericService<Activity>(db);
var progressService = new GenericService<Progress>(db);
var goalActivityService = new GenericService<GoalActivities>(db);


// ENDPOINTS

app.MapGet("/", () => "Backend funcionando!");

// Children
app.MapGet("/children", () => childService.GetAll());
app.MapGet("/children/{id}", (int id) => childService.GetById(id));
app.MapPost("/children", (Child c) => { childService.Insert(c); return Results.Created($"/children/{c.Id}", c); });
app.MapPut("/children/{id}", (int id, Child c) => { c.Id = id; childService.Update(c); return Results.Ok(c); });
app.MapDelete("/children/{id}", (int id) => { childService.Delete(id); return Results.Ok(); });

// Goals
app.MapGet("/goals", () => goalService.GetAll());
app.MapPost("/goals", (Goal g) => { goalService.Insert(g); return Results.Ok(g); });

// Activities
app.MapGet("/activities", () => activityService.GetAll());
app.MapPost("/activities", (Activity a) => { activityService.Insert(a); return Results.Ok(a); });

// Progress
app.MapGet("/progress", () => progressService.GetAll());
app.MapPost("/progress", (Progress p) => { progressService.Insert(p); return Results.Ok(p); });

// GoalActivities
app.MapGet("/goalActivities", () => goalActivityService.GetAll());
app.MapPost("/goalActivities", (GoalActivities ga) => { goalActivityService.Insert(ga); return Results.Ok(ga); });

app.Run();
