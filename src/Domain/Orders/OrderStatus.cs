using Domain.Shared.Entities;

namespace Domain.Orders;

public class OrderStatus
{
    public OrderStatusEnum Status { get; private set; }
    private Dictionary<OrderStatusEnum, List<OrderStatusEnum>> AllowedTransitions { get; set; } = new();
    
    private OrderStatus()
    {
        Status = OrderStatusEnum.Created;
        ConfigurePossibleTransitions();
    }
    
    public OrderStatus(OrderStatusEnum status)
    {
        Status = status;
        ConfigurePossibleTransitions();
    }
    
    public OrderStatus ToUpdated()
    {
        const OrderStatusEnum status = OrderStatusEnum.Updated;
        return TryCreateOrderStatus(status);
    }
    
    public OrderStatus ToCanceled()
    {
        const OrderStatusEnum status = OrderStatusEnum.Canceled;
        return TryCreateOrderStatus(status);
    }
    
    private OrderStatus TryCreateOrderStatus(OrderStatusEnum status)
    {
        VerifyTransition(status);
        return new OrderStatus(status);
    }
    
    private void ConfigurePossibleTransitions()
    {
        AllowedTransitions = new Dictionary<OrderStatusEnum, List<OrderStatusEnum>>
        {
            { OrderStatusEnum.Created, PossibleStatusWhenCreated() },
            { OrderStatusEnum.Updated, PossibleStatusWhenUpdated() },
            { OrderStatusEnum.Canceled, PossibleStatusWhenCanceled() }
        };
    }
    
    private static List<OrderStatusEnum> PossibleStatusWhenCreated() 
        => new() { OrderStatusEnum.Updated, OrderStatusEnum.Canceled };
    
    private static List<OrderStatusEnum> PossibleStatusWhenUpdated()
        => new() { OrderStatusEnum.Canceled };
    
    private static List<OrderStatusEnum> PossibleStatusWhenCanceled() => new();
    
    private void VerifyTransition(OrderStatusEnum newStatus)
    {
        if (!CanTransitionTo(newStatus))
            throw new InvalidOperationException($"Cannot change status from {Status} to {newStatus}");
    }
    
    private bool CanTransitionTo(OrderStatusEnum newStatus)
    {
        var transitions = AllowedTransitions[Status];
        return transitions.Contains(newStatus);
    }
}