using Domain.Shared.Contracts;
using Infrastructure.Contexts;
using Infrastructure.Repositories;

namespace UnitTest.TestHelpers.InMemoryDatabaseHelpers;

public class InMemoryRepositoryFactory
{
    public readonly OrderDbContext OrderDbContext;
    
    private InMemoryRepositoryFactory()
    {
        OrderDbContext = OrderDbContextInMemoryFactory.Create();
    }
    
    public static InMemoryRepositoryFactory GetInstance() => new();
    
    public IOrderRepository OrderRepository => new OrderRepository(OrderDbContext);
    public IProductRepository ProductRepository => new ProductRepository(OrderDbContext);
}