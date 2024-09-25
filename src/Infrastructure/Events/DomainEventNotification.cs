using Domain.Shared.Contracts;

namespace Infrastructure.Events;

public class DomainEventNotification : IDomainEventNotification
{
    public List<IDomainEvent> Events { get; } = new();

    public async Task SendAsync(IDomainEvent domainEvent)
    {
        Events.Add(domainEvent);
    }
}