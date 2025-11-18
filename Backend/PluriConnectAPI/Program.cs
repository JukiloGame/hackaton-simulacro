using LiteDB;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var db = new LiteDatabase(@"Data\pluriconnect.db");
var children = db.GetCollection<Child>("children");

app.MapGet("/", () => "Backend funcionando!");
app.MapGet("/children", () => children.FindAll());

app.Run();

public class Child
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Dob { get; set; }
}

