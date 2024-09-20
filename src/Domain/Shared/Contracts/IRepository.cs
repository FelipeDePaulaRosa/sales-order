using Domain.Shared.Entities;

namespace Domain.Shared.Contracts;

public interface IRepository<T, TKey> where T : AggregateRoot<TKey> where TKey : notnull
{
    Task<T> CreateAsync(T entity, bool saveChanges = true);
    Task UpdateAsync(T entity, bool saveChanges = true);
    Task SaveChangesAsync();
}