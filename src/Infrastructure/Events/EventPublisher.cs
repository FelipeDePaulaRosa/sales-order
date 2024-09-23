using Domain.Shared.Contracts;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Events;

public class EventPublisher : IEventPublisher
{
    private readonly ILogger _logger;

    public EventPublisher(ILogger logger)
    {
        _logger = logger;
    }

    public Task PublishAsync<TEvent>(TEvent @event) where TEvent : IDomainEvent
    {
        var eventType = @event.GetEventName();
        var data = JsonConvert.SerializeObject(@event);
        _logger.Information("Event: {EventType}, Data: {Data}", eventType, data);
        return Task.CompletedTask;
    }
}