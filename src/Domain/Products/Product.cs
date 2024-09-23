using Domain.Orders;
using Domain.Shared.Entities;
using Domain.Shared.ValueObjects;

namespace Domain.Products;

public class Product : AggregateRoot<Guid>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public bool Active { get; private set; }
    public Money UnitPrice { get; private set; }
    public Discount Discount { get; private set; }
    public int Stock { get; private set; }
    
    private Product() { }
    
    public Product(string code,
        string name,
        string brand,
        decimal amount,
        decimal discount,
        int stock)
    {
        Code = code;
        Name = name;
        Brand = brand;
        Active = true;
        UnitPrice = Money.FromDecimal(amount);
        Discount = new Discount(discount);
        Stock = stock;
    }
    
    public decimal GetPrice() => UnitPrice.GetValueFromCents();
    public decimal GetDiscount() => Discount.ValueInPercentage;
}