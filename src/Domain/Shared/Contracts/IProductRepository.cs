using Domain.Products;

namespace Domain.Shared.Contracts;

public interface IProductRepository : IRepository<Product, Guid>
{
    Task<List<Product>> GetProductsByIds(IEnumerable<Guid> ids);
}