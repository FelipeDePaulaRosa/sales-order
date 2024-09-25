using Domain.Shared.Entities;

namespace Domain.Orders.Entities;

public class OrderStatusHistory : Entity<Guid>
{
    public Guid OrderId { get; private set; }
    public string Message { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private OrderStatusHistory() { }
    
    public OrderStatusHistory(
        OrderStatusEnum status,
        string message = ""
    )
    {
        Message = message;
        CreatedAt = DateTime.UtcNow;
        Status = new OrderStatus(status);
    }
    
    public OrderStatusEnum GetStatusEnum() => Status.Status;
}