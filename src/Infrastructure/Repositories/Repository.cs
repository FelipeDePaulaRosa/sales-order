using Domain.Shared.Contracts;
using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<T, TKey> : IRepository<T, TKey> 
    where T : AggregateRoot<TKey> where TKey : notnull
{
    private readonly DbContext _context;
    protected readonly DbSet<T> DbSet;
    protected readonly IQueryable<T> DbSetNt;

    protected Repository(DbContext context)
    {
        _context = context;
        DbSet = context.Set<T>();
        DbSetNt = DbSet.AsNoTracking();
    }

    public async Task<T> CreateAsync(T entity, bool saveChanges = true)
    {
        var entry = await DbSet.AddAsync(entity);
        await FlushChangesAsync(saveChanges);
        return entry.Entity;
    }
    public async Task UpdateAsync(T entity, bool saveChanges = true)
    {
        DbSet.Update(entity);
        await FlushChangesAsync(saveChanges);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    private async Task FlushChangesAsync(bool saveChanges)
    {
        if (saveChanges)
            await _context.SaveChangesAsync();
    }
}