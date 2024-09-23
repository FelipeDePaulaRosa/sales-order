using Domain.Shared.Contracts;
using Newtonsoft.Json;

namespace Infrastructure.Events;

public class EventPublisher<TKey> : IEventPublisher<TKey>
    where TKey : notnull
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent<TKey>
    {
        var eventType = @event.GetEventName();
        var data = JsonConvert.SerializeObject(@event);
        Console.WriteLine($"Event: {eventType}, Data: {data}");
    }
}