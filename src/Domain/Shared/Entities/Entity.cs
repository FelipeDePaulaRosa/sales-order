using System.ComponentModel.DataAnnotations;
using Domain.Shared.Contracts;

namespace Domain.Shared.Entities;

public abstract class Entity<TKey> : IEntity<TKey>
    where TKey : notnull
{
    [Key]
    public TKey Id { get; protected set; } = default!;
    
    private List<IDomainEvent<TKey>> _domainEvents = new();
    public List<IDomainEvent<TKey>> DomainEvents => _domainEvents.ToList();

    protected void AddDomainEvent(IDomainEvent<TKey> domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}