namespace Application.Orders.Requests;

public record CreateOrderRequest
{
    public string Number { get; init; }
    public DateTime SaleDate { get; init; }
    public decimal Amount { get; init; }
    public Guid CustomerId { get; init; }
    public Guid MerchantId { get; init; }
}