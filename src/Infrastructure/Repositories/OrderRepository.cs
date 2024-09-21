using Domain.Orders;
using Domain.Shared.Contracts;
using Infrastructure.Contexts;

namespace Infrastructure.Repositories;

public class OrderRepository : Repository<Order, Guid>, IOrderRepository
{
    public OrderRepository(OrderDbContext context) : base(context)
    {
    }
}