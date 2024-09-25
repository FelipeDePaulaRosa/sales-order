using System.Text.Json.Serialization;
using Domain.Orders.Entities;
using Domain.Shared.Contracts;

namespace Domain.Orders.DomainEvents;

public class CancelOrderDomainEvent : IDomainEvent
{
    public string Id { get; set; }
    public string GetEventName() => "cancel_order_domain_event";

    [JsonConstructor]
    public CancelOrderDomainEvent() { }
    
    public CancelOrderDomainEvent(Order order)
    {
        Id = order.Id.ToString();
    }
}