using FluentValidation;

namespace Application.Orders.UseCases.CreateOrders;

public class CreateOrderValidation : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidation()
    {
        RuleFor(x => x.Number)
            .NotEmpty();
        
        //TODO: Add a custom validation for duplicated Number
        
        RuleFor(x => x.SaleDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("'SaleDate' must be greater than or equal to the current date");

        RuleFor(x => x.Amount)
            .NotNull();

        RuleFor(x => x.CustomerId)
            .NotNull();

        RuleFor(x => x.MerchantId)
            .NotNull();
    }
    
}