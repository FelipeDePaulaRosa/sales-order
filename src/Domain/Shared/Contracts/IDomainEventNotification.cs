namespace Domain.Shared.Contracts;

public interface IDomainEventNotification<TKey>
    where TKey : notnull
{
    public List<IDomainEvent<TKey>> Events { get; }
    Task SendAsync(IDomainEvent<TKey> domainEvent);
}