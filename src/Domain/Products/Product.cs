using Domain.Orders.Entities;
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
    
    public Product() { }
    
    public Product(
        Guid id,
        string code,
        string name,
        string brand,
        decimal amount,
        decimal discount,
        int stock,
        bool active)
    {
        Id = id;
        Code = code;
        Name = name;
        Brand = brand;
        UnitPrice = Money.FromDecimal(amount);
        Discount = new Discount(discount);
        Stock = stock;
        Active = active;
    }
    
    public decimal GetPrice() => UnitPrice.GetValueFromCents();
    public decimal GetDiscount() => Discount.ValueInPercentage;
}