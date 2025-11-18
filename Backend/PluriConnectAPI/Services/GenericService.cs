using LiteDB;
using System.Reflection;

public class GenericService<T> where T : new()
{
    private ILiteCollection<T> _col;

    public GenericService(LiteDatabase db)
    {
        // Nombre de la colección = nombre de la clase
        _col = db.GetCollection<T>(typeof(T).Name);
    }

    public IEnumerable<T> GetAll() => _col.FindAll();

    public T? GetById(int id)
    {
        var propId = typeof(T).GetProperty("Id");
        if(propId == null) return default;
        return _col.FindOne(x => (int)propId.GetValue(x)! == id);
    }

    public void Insert(T obj) => _col.Insert(obj);

    public bool Update(T obj) => _col.Update(obj);

   public bool Delete(int id)
    {
        return _col.Delete(id);
    }

}
