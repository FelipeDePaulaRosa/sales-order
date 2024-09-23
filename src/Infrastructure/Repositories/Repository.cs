using Domain.Shared.Contracts;
using Domain.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Repositories;

public class Repository<T, TKey> : IRepository<T, TKey> 
    where T : AggregateRoot<TKey> where TKey : notnull
{
    private readonly DbContext _context;
    protected readonly DbSet<T> DbSet;
    protected readonly IQueryable<T> DbSetNt;
    private readonly IDomainEventNotification _domainEventNotification;

    protected Repository(DbContext context, IDomainEventNotification domainEventNotification)
    {
        _context = context;
        _domainEventNotification = domainEventNotification;
        DbSet = context.Set<T>();
        DbSetNt = DbSet.AsNoTracking();
    }

    public async Task<T> CreateAsync(T entity, bool saveChanges = true)
    {
        var entry = await DbSet.AddAsync(entity);
        await FlushChangesAsync(saveChanges);
        var createdEntity = entry.Entity;
        await PublishDomainEvents(createdEntity);
        return createdEntity;
    }

    public async Task<List<T>> CreateRangeAsync(List<T> entities, bool saveChanges = true)
    {
        List<EntityEntry<T>> entries = new();
        entities.ForEach(x =>
        {
            entries.Add(DbSet.Add(x)); 
        });
        await FlushChangesAsync(saveChanges);
        foreach (var entry in entries)
        {
            await PublishDomainEvents(entry.Entity);
        }
        return entities;
    }

    public async Task UpdateAsync(T entity, bool saveChanges = true)
    {
        var entry = DbSet.Update(entity);
        await FlushChangesAsync(saveChanges);
        await PublishDomainEvents(entry.Entity);
    }
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public DbSet<T> GetDbSet() => DbSet;

    private async Task FlushChangesAsync(bool saveChanges)
    {
        if (saveChanges)
            await _context.SaveChangesAsync();
    }
    
    private async Task PublishDomainEvents(T entity)
    {
        var domainEvents = entity.DomainEvents.ToList();
        domainEvents.ForEach(x => x.Id = entity.Id.ToString());
        entity.ClearDomainEvents();
        foreach (var domainEvent in domainEvents)
        {
            await _domainEventNotification.SendAsync(domainEvent);
        }
    }
}