using Domain.Orders.Entities;
using Domain.Shared.Contracts;
using Domain.Shared.Exceptions;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OrderRepository : Repository<Order, Guid>, IOrderRepository
{
    public OrderRepository(OrderDbContext context,
        IDomainEventNotification domainEventNotification) : base(context, domainEventNotification)
    {
    }

    public async Task<Order?> GetOrderOrDefaultByNumberNoTrackAsync(string number)
    {
        return await DbSetNt
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Number == number);
    }

    public async Task<Order?> GetOrderByIdOrDefaultNoTrackAsync(Guid id)
    {
        return await DbSetNt
            .Include(x=> x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Order> GetOrderByIdAsync(Guid requestId)
    {
        return await DbSet
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == requestId) 
            ?? throw new SalesOrderNotFoundException($"Order with id: {requestId} not found");
    }
}