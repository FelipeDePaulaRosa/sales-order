using Application.Orders.UseCases.CreateOrders;
using Domain.Orders;
using Domain.Shared.Contracts;
using FluentAssertions;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;
using Xunit;

namespace UnitTest.Application.Orders.CreateOrders;

public class CreateOrderHandlerTest
{
    private readonly CreateOrderHandler _handler;
    private readonly IOrderRepository _orderRepository;

    public CreateOrderHandlerTest()
    {
        var inMemoryRepositoryFactory = InMemoryRepositoryFactory.GetInstance();
        _orderRepository = inMemoryRepositoryFactory.OrderRepository;
        _handler = new CreateOrderHandler(_orderRepository);
    }

    [Fact]
    public async Task Should_Create_Order_Successfully()
    {
        var request = new CreateOrderRequest
        {
            Number = "000000000000000000X1",
            SaleDate = DateTime.UtcNow,
            Amount = 2.5m,
            CustomerId = Guid.NewGuid(),
            MerchantId = Guid.NewGuid()
        };

        var response = await _handler.Handle(request, CancellationToken.None);
        
        var createdOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(response.Id);

        createdOrder.Should().NotBeNull();
        if(createdOrder == null) return;
        createdOrder.Number.Should().Be(request.Number);
        createdOrder.SaleDate.Should().Be(request.SaleDate);
        createdOrder.GetAmountValue().Should().Be(request.Amount);
        createdOrder.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Created);
        createdOrder.CustomerId.Should().Be(request.CustomerId);
        createdOrder.MerchantId.Should().Be(request.MerchantId);
    }
}