using Domain.Shared.Contracts;
using MediatR;

namespace Application.Orders.UseCases.CancelOrder;

public class CancelOrderHandler : IRequestHandler<CancelOrderRequest, CancelOrderResponse>
{
    private readonly IOrderRepository _orderRepository;

    public CancelOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<CancelOrderResponse> Handle(CancelOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        if (order.IsCanceled)
            return new CancelOrderResponse();
        order.CancelOrder();
        await _orderRepository.UpdateAsync(order);
        return new CancelOrderResponse();
    }
}