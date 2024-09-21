using Domain.Orders;
using Domain.Orders;

namespace Domain.Shared.Contracts;

public interface IOrderRepository : IRepository<Order, Guid>
{
}