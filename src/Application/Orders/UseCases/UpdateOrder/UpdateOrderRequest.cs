using System.Text.Json.Serialization;
using MediatR;

namespace Application.Orders.UseCases.UpdateOrder;

public record UpdateOrderRequest : IRequest<UpdateOrderResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Number { get; init; }
    public DateTime SaleDate { get; init; }
    public Guid CustomerId { get; init; }
    public Guid MerchantId { get; init; }
    public List<UpdateOrderProductRequest> Products { get; init; }
}

public record UpdateOrderProductRequest
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public bool IsCanceled { get; init; }
}