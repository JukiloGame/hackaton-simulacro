using PluriConnectAPI.Services;

public static class CrudExtensions
{
    public static void MapCrud<T>(this IEndpointRouteBuilder app, string route, GenericService<T> service) where T : new()
    {
        // GET ALL
        app.MapGet($"/{route}", () => service.GetAll());

        // GET BY ID
        app.MapGet($"/{route}/{{id}}", (int id) => service.GetById(id));

        // CREATE
        app.MapPost($"/{route}", (T entity) =>
        {
            service.Insert(entity);
            return Results.Created($"/{route}/{GetId(entity)}", entity);
        });

        // UPDATE
        app.MapPut($"/{route}/{{id}}", (int id, T entity) =>
        {
            SetId(entity, id);
            service.Update(entity);
            return Results.Ok(entity);
        });

        // DELETE
        app.MapDelete($"/{route}/{{id}}", (int id) =>
        {
            service.Delete(id);
            return Results.Ok();
        });
    }

    // Helpers para leer/escribir Id sin saber el tipo:
    private static int GetId<T>(T obj)
    {
        var prop = typeof(T).GetProperty("Id");
        return (int)(prop?.GetValue(obj) ?? 0);
    }

    private static void SetId<T>(T obj, int id)
    {
        var prop = typeof(T).GetProperty("Id");
        prop?.SetValue(obj, id);
    }
}
