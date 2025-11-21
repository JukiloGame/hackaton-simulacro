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
var commentService = new GenericService<Comment>(db);


// ENDPOINTS

app.MapGet("/", () => "Backend funcionando!");

// Llama al CRUD para mapear los endpoints automáticamente
app.MapCrud("children", childService);
app.MapCrud("goals", goalService);
app.MapCrud("activities", activityService);
app.MapCrud("progress", progressService);
app.MapCrud("goalActivities", goalActivityService);
app.MapCrud("children/comments", commentService);

//TODO: Añadir MapPost para activities asociadas al niño y crear comentarios

app.Run();
