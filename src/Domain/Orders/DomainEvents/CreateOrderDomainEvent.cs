﻿using System.Text.Json.Serialization;
using Domain.Orders.Entities;
using Domain.Shared.Contracts;

namespace Domain.Orders.DomainEvents;

public class CreateOrderDomainEvent : IDomainEvent
{
    public string Id { get; set; }
    public string GetEventName() => "Order created";

    [JsonConstructor]
    public CreateOrderDomainEvent() { }
    
    public CreateOrderDomainEvent(Order order)
    {
        Id = order.Id.ToString();
    }
}