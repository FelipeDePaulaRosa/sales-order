using Domain.Orders;

namespace Application.Orders.UseCases.CreateOrders;

public record CreateOrderResponse
{
    public Guid Id { get; init; }
    public string Number { get; init; }
    public DateTime SaleDate { get; init; }
    public decimal Amount { get; init; }
    public OrderStatusEnum Status { get; init; }
    public Guid CustomerId { get; init; }
    public Guid MerchantId { get; init; }
}