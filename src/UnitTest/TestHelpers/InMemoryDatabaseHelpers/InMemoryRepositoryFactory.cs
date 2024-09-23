using Domain.Shared.Contracts;
using Infrastructure.Contexts;
using Infrastructure.Events;
using Infrastructure.Repositories;

namespace UnitTest.TestHelpers.InMemoryDatabaseHelpers;

public class InMemoryRepositoryFactory
{
    public readonly OrderDbContext OrderDbContext;
    private readonly IDomainEventNotification _domainEventNotification;
    
    private InMemoryRepositoryFactory()
    {
        OrderDbContext = OrderDbContextInMemoryFactory.Create();
        _domainEventNotification = new DomainEventNotification();
    }
    
    public static InMemoryRepositoryFactory GetInstance() => new();
    
    public IOrderRepository OrderRepository => new OrderRepository(OrderDbContext, _domainEventNotification);
    public IProductRepository ProductRepository => new ProductRepository(OrderDbContext, _domainEventNotification);
}