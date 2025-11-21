using Microsoft.EntityFrameworkCore;
using PluriConnectAPI.Data;

namespace PluriConnectAPI.Services;

public class GenericService<T> where T : class
{
    private readonly AppDbContext _ctx;
    private readonly DbSet<T> _dbSet;

    public GenericService(AppDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = ctx.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

    // Assumes PK is int and named Id
    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task InsertAsync(T entity)
    {
        _dbSet.Add(entity);
        await _ctx.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return await _ctx.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var e = await GetByIdAsync(id);
        if (e == null) return false;
        _dbSet.Remove(e);
        return await _ctx.SaveChangesAsync() > 0;
    }
}
