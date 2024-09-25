using System.Text.Json.Serialization;
using MediatR;

namespace Application.Orders.UseCases.UpdateOrder;

public class UpdateOrderRequest : IRequest<UpdateOrderResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Number { get; set; }
    public DateTime SaleDate { get; set; }
    public Guid CustomerId { get; set; }
    public Guid MerchantId { get; set; }
    public List<UpdateOrderProductRequest> Products { get; set; } = new();
}

public class UpdateOrderProductRequest
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsCanceled { get; set; }
}