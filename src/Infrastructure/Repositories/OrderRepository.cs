using Domain.Orders;
using Domain.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : Repository<Order, Guid>, IOrderRepository
{
    protected OrderRepository(DbContext context) : base(context)
    {
    }
}