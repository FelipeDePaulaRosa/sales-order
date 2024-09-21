using Application.Orders.Requests;

namespace Application.Orders.Services.Contracts;

public interface IOrderService
{
    public Task CreateOrder(CreateOrderRequest request);
}