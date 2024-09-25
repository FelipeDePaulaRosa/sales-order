using System.ComponentModel.DataAnnotations;
using Domain.Shared.Contracts;

namespace Domain.Shared.Entities;

public abstract class Entity<TKey> : IEntity<TKey>
    where TKey : notnull
{
    [Key]
    public TKey Id { get; protected set; } = default!;
    
    private List<IDomainEvent> _domainEvents = new();
    public List<IDomainEvent> DomainEvents => _domainEvents.ToList();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}