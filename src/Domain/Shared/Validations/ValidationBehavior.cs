using CrossCutting.FluentValidationNotifications;
using FluentValidation;
using MediatR;

namespace Domain.Shared.Validations;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator> _validators;
    private readonly IFluentValidationNotificationContext _fluentValidationNotificationContext;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IFluentValidationNotificationContext fluentValidationNotificationContext)
    {
        _validators = validators;
        _fluentValidationNotificationContext = fluentValidationNotificationContext;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestToValidate = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(requestToValidate))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();

        if (failures.Count == 0)
            return await next();
        
        var errors = failures.Select(x => x.ErrorMessage);
        _fluentValidationNotificationContext.AddNotifications(errors);
        return default!;
    }
}