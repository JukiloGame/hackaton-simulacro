using LiteDB;
using System.Reflection;

namespace PluriConnectAPI.Services;

public class GenericService<T> where T : new()
{

    private ILiteCollection<T> _col;

    public GenericService(LiteDatabase db)
    {
        // Nombre de la colección = nombre de la clase
        _col = db.GetCollection<T>(GetCollectionName());
    }
        private static string GetCollectionName()
    {
        // Convierte "Child" -> "children", "Goal" -> "goals", etc.
        return typeof(T).Name.ToLower() + "s";
    }

    public IEnumerable<T> GetAll() => _col.FindAll();
    public T? GetById(int id) => _col.FindById(id);
    public void Insert(T obj) => _col.Insert(obj);
    public bool Update(T obj) => _col.Update(obj);
    public bool Delete(int id) { return _col.Delete(id); }

}
