using Domain.Orders.DomainEvents;
using Domain.Shared.Entities;

namespace Domain.Orders.Entities;

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

    public Order()
    {
    }

    public Order(
        string number,
        DateTime saleDate,
        Guid customerId,
        Guid merchantId)
    {
        Number = number;
        SaleDate = saleDate;
        MarkStatusAsCreated();
        IsCanceled = false;
        CustomerId = customerId;
        MerchantId = merchantId;
    }

    public void AddProduct(Guid productId, int quantity, decimal unitPrice, decimal discount)
    {
        Products.Add(new OrderProduct(
            productId,
            quantity,
            unitPrice,
            discount));
    }

    public decimal GetAmountValue() => Amount.GetValueFromCents();

    public OrderStatusEnum GetCurrentStatusEnum() => Status.Status;

    public void MarkStatusAsCreated()
    {
        Status = new OrderStatus(OrderStatusEnum.Created);
        AddStatusHistory(OrderStatusEnum.Created);
        AddDomainEvent(new CreateOrderDomainEvent(this));
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

    public void CalcAmount()
    {
        var amount = Products.Sum(x => x.Amount.GetValueFromCents());
        Amount = Money.FromDecimal(amount);
    }
}