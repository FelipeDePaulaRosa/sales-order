using Domain.Orders.DomainEvents;
using Domain.Products.DomainEvents;
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

    public Order(){ }

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
        
        RemoveStockEvent(productId, quantity);
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
        AddDomainEvent(new UpdateOrderDomainEvent(this));
    }

    public void MarkStatusAsCanceled()
    {
        Status = Status.ToCanceled();
        IsCanceled = true;
        AddStatusHistory(OrderStatusEnum.Canceled);
        AddDomainEvent(new CancelOrderDomainEvent(this));
    }

    private void AddStatusHistory(OrderStatusEnum status)
        => StatusHistory.Add(new OrderStatusHistory(status));

    public void CalcAmount()
    {
        var amount = Products
            .Where(x => !x.IsCanceled)
            .Sum(x => x.Amount.GetValueFromCents());
        Amount = Money.FromDecimal(amount);
    }

    public void UpdateOrder(string number,
        DateTime saleDate,
        Guid customerId,
        Guid merchantId)
    {
        Number = number;
        SaleDate = saleDate;
        CustomerId = customerId;
        MerchantId = merchantId;
        MarkStatusAsUpdated();
    }

    public void UpdateProduct(Guid id, int quantity, bool isCanceled)
    {
        var product = Products.First(x => x.Id == id);
        AddUpdateProductEvent(product, quantity, isCanceled);
        product.Update(quantity, isCanceled);
    }
    
    public void CancelOrder()
    {
        MarkStatusAsCanceled();
        Products.ForEach(x => AddStockEvent(x.ProductId, x.Quantity));
    }

    private void AddUpdateProductEvent(OrderProduct product, int quantity, bool isCanceled)
    {
        if (isCanceled != product.IsCanceled)
        {
            if (isCanceled) AddStockEvent(product.Id, product.Quantity);
            else RemoveStockEvent(product.Id, product.Quantity);
        }
        else
        {
            var diff = Math.Abs(product.Quantity - quantity);
            if(diff == 0) return;
            if (product.Quantity < quantity)
                RemoveStockEvent(product.Id, diff);
            else if (product.Quantity > quantity)
                AddStockEvent(product.Id, diff);
        }
    }
    
    private void AddStockEvent(Guid productId, int quantity)
        => AddDomainEvent(new AddStockOfProductDomainEvent(this, productId, quantity));

    private void RemoveStockEvent(Guid productId, int quantity)
        => AddDomainEvent(new RemoveStockOfProductDomainEvent(this, productId, quantity));
}
