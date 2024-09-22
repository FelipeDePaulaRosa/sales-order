using Domain.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.UseCases.CreateOrders;

public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    private readonly IOrderRepository _orderRepository;
    
    public CreateOrderRequestValidator(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
        
        RuleFor(x => x.Number)
            .NotEmpty();
        
        RuleFor(x => x)
            .Must(NumberIsUnique)
            .WithMessage("One order with the same number already exists");
        
        RuleFor(x => x.SaleDate)
            .Must(x => x.Date.Date >= DateTime.UtcNow.Date)
            .WithMessage("'SaleDate' must be greater than or equal to the current date");

        RuleFor(x => x.Amount)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.CustomerId)
            .NotNull();

        RuleFor(x => x.MerchantId)
            .NotNull();
    }
    
    private bool NumberIsUnique(CreateOrderRequest request)
        => !_orderRepository
            .GetDbSet()
            .AsNoTracking()
            .Any(x => x.Number == request.Number);
}