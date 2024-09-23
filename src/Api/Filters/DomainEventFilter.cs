using Domain.Shared.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class DomainEventFilter : ActionFilterAttribute
{
    private readonly IDomainEventNotification<Guid> _domainEventNotification;
    private readonly IEventPublisher<Guid> _eventPublisher;

    public DomainEventFilter(IDomainEventNotification<Guid> domainEventNotification, IEventPublisher<Guid> eventPublisher)
    {
        _domainEventNotification = domainEventNotification;
        _eventPublisher = eventPublisher;
    }

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var resultContext = await next();

        if (resultContext.Exception == null || resultContext.ExceptionHandled)
        {
            var events = _domainEventNotification.Events;
            events.ForEach(x => _eventPublisher.PublishAsync(x));
        }
    }
}