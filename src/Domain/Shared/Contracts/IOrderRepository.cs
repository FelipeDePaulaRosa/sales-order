using Domain.Orders;

namespace Domain.Shared.Contracts;

public interface IOrderRepository : IRepository<Order, Guid>
{
    Task<Order?> GetOrderOrDefaultByNumberNoTrackAsync(string orderNumber);
}