using Domain.Products;
using Domain.Shared.Entities;
using Domain.Shared.ValueObjects;

namespace Domain.Orders;

public class OrderProduct : Entity<Guid>
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; }
    public Discount Discount { get; private set; }
    public Money Amount { get; private set; }
    public bool IsCanceled { get; private set; }
    
    public Product Product { get; private set; }
    
    private OrderProduct() { }
    
    public OrderProduct(Guid productId, int quantity, decimal unitPrice, decimal discount)
    {
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = Money.FromDecimal(unitPrice);
        Discount = new Discount(discount);
        Amount = CalcTotalPrice();
        IsCanceled = false;
    }
    
    private Money CalcTotalPrice()
        => Money.FromDecimal(GetValueAppliedDiscount * Quantity);
    
    private decimal GetValueAppliedDiscount => Discount.ApplyDiscount(UnitPrice.GetValueFromCents());
}