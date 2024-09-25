using Domain.Orders.Entities;

namespace Domain.Shared.Contracts;

public interface IOrderRepository : IRepository<Order, Guid>
{
    Task<Order?> GetOrderOrDefaultByNumberNoTrackAsync(string number);
    Task<Order?> GetOrderByIdOrDefaultNoTrackAsync(Guid id);
    Task<Order> GetOrderByIdAsync(Guid requestId);
}