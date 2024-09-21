using Domain.Orders;
using Domain.Shared.Contracts;
using MediatR;

namespace Application.Orders.UseCases.CreateOrders;

public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    
    public CreateOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Order(
            request.Number,
            request.SaleDate,
            request.Amount,
            request.CustomerId,
            request.MerchantId);
        
        var response = await _orderRepository.CreateAsync(order);
        
        return new CreateOrderResponse
        {
            Id = response.Id,
            Number = response.Number,
            SaleDate = response.SaleDate,
            Amount = response.GetAmountValue(),
            Status = response.GetCurrentStatusEnum(),
            CustomerId = response.CustomerId,
            MerchantId = response.MerchantId
        };
    }
}