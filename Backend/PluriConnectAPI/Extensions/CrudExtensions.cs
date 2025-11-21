using PluriConnectAPI.Services;

namespace PluriConnectAPI.Extensions;

public static class CrudExtensions
{
    public static void MapCrudAsync<T>(this IEndpointRouteBuilder app, string route, GenericService<T> service) where T : class, new()
    {
        // GET all
        app.MapGet($"/{route}", async () => await service.GetAllAsync());

        // GET by id
        app.MapGet($"/{route}/{{id}}", async (int id) =>
        {
            var item = await service.GetByIdAsync(id);
            return item != null ? Results.Ok(item) : Results.NotFound();
        });

        // POST create
        app.MapPost($"/{route}", async (T entity) =>
        {
            await service.InsertAsync(entity);
            var id = GetId(entity);
            return Results.Created($"/{route}/{id}", entity);
        });

        // PUT update
        app.MapPut($"/{route}/{{id}}", async (int id, T entity) =>
        {
            SetId(entity, id);
            await service.UpdateAsync(entity);
            return Results.Ok(entity);
        });

        // DELETE
        app.MapDelete($"/{route}/{{id}}", async (int id) =>
        {
            await service.DeleteByIdAsync(id);
            return Results.Ok();
        });
    }

    // helpers: assume int Id property
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
