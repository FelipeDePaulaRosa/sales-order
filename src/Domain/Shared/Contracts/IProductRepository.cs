using Domain.Products.Entities;

namespace Domain.Shared.Contracts;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<List<Product>> GetProductsByIds(IEnumerable<Guid> ids);
}