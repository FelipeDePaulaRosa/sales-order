using Domain.Shared.Contracts;
using Newtonsoft.Json;

namespace Infrastructure.Events;

public class EventPublisher : IEventPublisher
{
    public async Task PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
        var eventType = @event.GetEventName();
        var data = JsonConvert.SerializeObject(@event);
        Console.WriteLine($"Event: {eventType}, Data: {data}");
    }
}