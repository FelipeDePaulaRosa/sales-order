using Domain.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.UseCases.CreateOrders;

public class CreateOrderValidation : AbstractValidator<CreateOrderRequest>
{
    private readonly IOrderRepository _orderRepository;
    
    public CreateOrderValidation(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        
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
    
    private bool BeUniqueNumber(string number)
    {
        return !_orderRepository
            .GetDbSet()
            .AsNoTracking()
            .Any(x => x.Number == number);
    }
}