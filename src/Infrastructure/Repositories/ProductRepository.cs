using Domain.Products.Entities;
using Domain.Shared.Contracts;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : Repository<Product, Guid>, IProductRepository
{
    public ProductRepository(OrderDbContext context,
        IDomainEventNotification domainEventNotification) : base(context, domainEventNotification)
    {
    }

    public async Task<List<Product>> GetProductsByIds(IEnumerable<Guid> ids)
    {
        return await DbSet
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }
}