using MediatR;

namespace Application.Orders.UseCases.CreateOrders;

public record CreateOrderRequest : IRequest<CreateOrderResponse>
{
    public string Number { get; init; }
    public DateTime SaleDate { get; init; }
    public Guid CustomerId { get; init; }
    public Guid MerchantId { get; init; }
    public List<CreateOrderProductRequest> Products { get; init; }
}

public record CreateOrderProductRequest
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
}