using Domain.Shared.Contracts;

namespace Infrastructure.Events;

public class DomainEventNotification<TKey> : IDomainEventNotification<TKey>
    where TKey : notnull
{
    public List<IDomainEvent<TKey>> Events { get; } = new();

    public async Task SendAsync(IDomainEvent<TKey> domainEvent)
    {
        Events.Add(domainEvent);
    }
}