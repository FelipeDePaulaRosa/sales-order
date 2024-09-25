using System.Text.Json.Serialization;
using Domain.Orders.Entities;
using Domain.Shared.Contracts;

namespace Domain.Orders.DomainEvents;

public class UpdateOrderDomainEvent : IDomainEvent
{
    public string Id { get; set; }
    public string GetEventName() => "update_order_domain_event";

    [JsonConstructor]
    public UpdateOrderDomainEvent() { }
    
    public UpdateOrderDomainEvent(Order order)
    {
        Id = order.Id.ToString();
    }
}