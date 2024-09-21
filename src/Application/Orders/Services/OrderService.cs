using Application.Orders.Requests;
using Application.Orders.Services.Contracts;
using Domain.Orders;
using Domain.Shared.Contracts;

namespace Application.Orders.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    public Task CreateOrder(CreateOrderRequest request)
    {
        var order = new Order(
            request.Number,
            request.SaleDate,
            request.Amount,
            request.CustomerId,
            request.MerchantId);
        
        return _orderRepository.CreateAsync(order);
    }
}