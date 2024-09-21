using Domain.Orders;
using Domain.Shared.Contracts;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : Repository<Order, Guid>, IOrderRepository
{
    public OrderRepository(OrderDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetOrderOrDefaultByNumberNoTrackAsync(string orderNumber)
    {
        return await DbSetNt.FirstOrDefaultAsync(x => x.Number == orderNumber);
    }
}