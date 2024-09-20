using Domain.Shared.Entities;

namespace Domain.Orders;

public class Order : AggregateRoot<Guid>
{
    public int Number { get; private set; }
    public DateTime SaleDate { get; private set; }
    public Money Amount { get; private set; }
    public OrderStatus Status { get; private set; }
    public bool IsCanceled { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid MerchantId { get; private set; }
    public List<OrderStatusHistory> StatusHistory { get; } = new();
    
    private Order() { }
    
    public Order(
        int number,
        DateTime saleDate,
        decimal amount,
        bool isCanceled,
        Guid customerId,
        Guid merchantId)
    {
        Number = number;
        SaleDate = saleDate;
        Amount = Money.FromDecimal(amount);
        MarkStatusAsCreated();
        IsCanceled = isCanceled;
        CustomerId = customerId;
        MerchantId = merchantId;
        
    }
    
    private void MarkStatusAsCreated()
    {
        Status = new OrderStatus(OrderStatusEnum.Created);
        AddStatusHistory(OrderStatusEnum.Created);
    }

    public void MarkStatusAsUpdated()
    {
        Status = Status.ToUpdated();
        AddStatusHistory(OrderStatusEnum.Updated);
    }
    
    public void MarkStatusAsCancelled()
    {
        Status = Status.ToCanceled();
        IsCanceled = true;
        AddStatusHistory(OrderStatusEnum.Updated);
    }
    
    private void AddStatusHistory(OrderStatusEnum status)
    {
        StatusHistory.Add(new OrderStatusHistory(status));
    }
}