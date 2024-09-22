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

    public async Task<Order?> GetOrderOrDefaultByNumberNoTrackAsync(string number)
    {
        return await DbSetNt.FirstOrDefaultAsync(x => x.Number == number);
    }

    public async Task<Order?> GetOrderByIdOrDefaultNoTrackAsync(Guid id)
    {
        return await DbSetNt.FirstOrDefaultAsync(x => x.Id == id);
    }
}