namespace Domain.Shared.Contracts;

public interface IDomainEventNotification
{
    public List<IDomainEvent> Events { get; }
    Task SendAsync(IDomainEvent domainEvent);
}