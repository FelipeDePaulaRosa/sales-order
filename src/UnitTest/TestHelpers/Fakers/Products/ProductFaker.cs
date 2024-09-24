using Bogus;
using Domain.Orders.Entities;
using Domain.Products;
using Domain.Shared.ValueObjects;

namespace UnitTest.TestHelpers.Fakers.Products;

public static class ProductFaker
{
    public static Product GenerateProduct(bool active = true)
    {
        var faker = new Faker<Product>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(o => o.Code, f => f.Random.AlphaNumeric(20))
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Brand, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Active, active)
            .RuleFor(p => p.UnitPrice, f => Money.FromDecimal(f.Random.Decimal(100, 1000)))
            .RuleFor(p => p.Discount, f => new Discount(f.Random.Decimal(0, 50)))
            .RuleFor(p => p.Stock, f => f.Random.Int(1, 100));

        return faker.Generate();
    }
    
    public static List<Product> GenerateSpecificCountOfProducts(int count = 10, bool active = true)
    {
        List<Product> products = new();
        for (var i = 0; i < count; i++)
        {
            products.Add(GenerateProduct(active));
        }
        return products;
    }
}