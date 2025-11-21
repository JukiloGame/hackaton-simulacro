using PluriConnectAPI.Services;

namespace PluriConnectAPI.Extensions;

public static class CrudExtensions
{
    public static void MapCrudAsync<T>(this IEndpointRouteBuilder app, string route) where T : class, new()
    {
        // GET all
        app.MapGet($"/{route}", async (GenericService<T> service) =>
        {
            try
            {
                var list = await service.GetAllAsync();
                return Results.Ok(list);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // GET by id
        app.MapGet($"/{route}/{{id}}", async (int id, GenericService<T> service) =>
        {
            try
            {
                var item = await service.GetByIdAsync(id);
                return item != null ? Results.Ok(item) : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // POST create
        app.MapPost($"/{route}", async (T entity, GenericService<T> service) =>
        {
            try
            {
                await service.InsertAsync(entity);
                var id = GetId(entity);
                return Results.Created($"/{route}/{id}", entity);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // PUT update
        app.MapPut($"/{route}/{{id}}", async (int id, T entity, GenericService<T> service) =>
        {
            try
            {
                // Ensure resource exists
                var existing = await service.GetByIdAsync(id);
                if (existing == null) return Results.NotFound();

                SetId(entity, id);
                var updated = await service.UpdateAsync(entity);
                if (!updated) return Results.Problem("No se pudo actualizar el recurso.");

                return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });

        // DELETE
        app.MapDelete($"/{route}/{{id}}", async (int id, GenericService<T> service) =>
        {
            try
            {
                var deleted = await service.DeleteByIdAsync(id);
                return deleted ? Results.NoContent() : Results.NotFound();
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
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
