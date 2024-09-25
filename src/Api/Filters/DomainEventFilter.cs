using Domain.Shared.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class DomainEventFilter : ActionFilterAttribute
{
    private readonly IDomainEventNotification _domainEventNotification;
    private readonly IEventPublisher _eventPublisher;

    public DomainEventFilter(IDomainEventNotification domainEventNotification, IEventPublisher eventPublisher)
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