using Domain.Products;
using Domain.Shared.Contracts;

namespace UnitTest.TestHelpers.SeedInjectors.Products;

public static class ProductSeedInjector
{
    public static List<Product> Inject(IProductRepository repository)
        => repository.CreateRangeAsync(GetProducts()).Result;
    
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new("123", "Product 1", "Brand 1", 100, 0, 10),
            new("456", "Product 2", "Brand 2", 200, 0, 20),
            new("789", "Product 3", "Brand 3", 300, 0, 30)
        };
    }
}