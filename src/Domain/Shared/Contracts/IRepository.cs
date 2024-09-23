using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Shared.Contracts;

public interface IRepository<T, TKey> where T : AggregateRoot<TKey> where TKey : notnull
{
    Task<T> CreateAsync(T entity, bool saveChanges = true);
    Task<List<T>> CreateRangeAsync(List<T> entities, bool saveChanges = true);
    Task UpdateAsync(T entity, bool saveChanges = true);
    Task SaveChangesAsync();
    DbSet<T> GetDbSet();
}