using Bogus;
using Domain.Orders.Entities;
using Domain.Products.Entities;

namespace UnitTest.TestHelpers.Fakers.Orders;

public static class OrderFaker
{
    public static Order GenerateOrderAsCreated(Guid id, List<Product> products)
    {
        var faker = new Faker<Order>()
            .RuleFor(o => o.Id, id)
            .RuleFor(o => o.Number, f => f.Random.AlphaNumeric(20))
            .RuleFor(o => o.SaleDate, f => f.Date.Future())
            .RuleFor(o => o.IsCanceled, false)
            .RuleFor(o => o.StatusHistory, new List<OrderStatusHistory>{ new (OrderStatusEnum.Created) })
            .RuleFor(o => o.CustomerId, Guid.NewGuid())
            .RuleFor(o => o.MerchantId, Guid.NewGuid());
        
        var order =  faker.Generate();
        
        order.MarkStatusAsCreated();
        
        products.ForEach(p =>
        {
            var quantity = new Random().Next(p.Stock);
            order.AddProduct(p.Id, quantity, p.UnitPrice.GetValueFromCents(), p.GetDiscount());
        });
        
        order.CalcAmount();
        
        return order;
    }
}