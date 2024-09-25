using MediatR;

namespace Application.Orders.UseCases.CancelOrder;

public class CancelOrderRequest : IRequest<CancelOrderResponse>
{
    public Guid Id { get; set; }
}