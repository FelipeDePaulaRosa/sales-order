using System.Text.Json.Serialization;
using Domain.Orders.Entities;
using Domain.Shared.Contracts;

namespace Domain.Products.DomainEvents;

public class RemoveStockOfProductDomainEvent : IDomainEvent
{
    public string Id { get; set; }
    public int Quantity { get; set; }
    public string ProductId { get; set; }
    public string GetEventName() => "remove_stock_of_product_domain_event";
    
    [JsonConstructor]
    public RemoveStockOfProductDomainEvent() { }
    
    public RemoveStockOfProductDomainEvent(Order order, Guid productId, int quantity)
    {
        Id = order.Id.ToString();
        ProductId = productId.ToString();
        Quantity = quantity;
    }
}