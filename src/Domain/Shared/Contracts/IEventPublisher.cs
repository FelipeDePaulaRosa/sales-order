namespace Domain.Shared.Contracts;

public interface IEventPublisher<TKey> where TKey : notnull
{
    Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : IDomainEvent<TKey>;
}