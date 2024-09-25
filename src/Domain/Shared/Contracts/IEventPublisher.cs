namespace Domain.Shared.Contracts;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : IDomainEvent;
}