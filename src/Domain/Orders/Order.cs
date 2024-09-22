using Domain.Shared.Entities;

namespace Domain.Orders;

public class Order : AggregateRoot<Guid>
{
    public string Number { get; private set; }
    public DateTime SaleDate { get; private set; }
    public Money Amount { get; private set; }
    public OrderStatus Status { get; private set; }
    public bool IsCanceled { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid MerchantId { get; private set; }
    public List<OrderStatusHistory> StatusHistory { get; } = new();
    public List<OrderProduct> Products { get; } = new();
    //TODO: List<Discount> OrderDiscounts { get; } = new();

    private Order() { }

    public Order(
        string number,
        DateTime saleDate,
        decimal amount,
        Guid customerId,
        Guid merchantId)
    {
        Number = number;
        SaleDate = saleDate;
        Amount = Money.FromDecimal(amount); //TODO: Calc amount from products and discounts
        MarkStatusAsCreated();
        IsCanceled = false;
        CustomerId = customerId;
        MerchantId = merchantId;
    }
    
    public decimal GetAmountValue() => Amount.GetValueFromCents();
    
    public OrderStatusEnum GetCurrentStatusEnum() => Status.Status;

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

    public void MarkStatusAsCanceled()
    {
        Status = Status.ToCanceled();
        IsCanceled = true;
        AddStatusHistory(OrderStatusEnum.Canceled);
    }

    private void AddStatusHistory(OrderStatusEnum status)
        => StatusHistory.Add(new OrderStatusHistory(status));
}