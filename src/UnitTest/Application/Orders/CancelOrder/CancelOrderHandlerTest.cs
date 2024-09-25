using Application.Orders.UseCases.CancelOrder;
using Domain.Orders.Entities;
using Domain.Shared.Contracts;
using Domain.Shared.Exceptions;
using FluentAssertions;
using UnitTest.TestHelpers.Fakers.Orders;
using UnitTest.TestHelpers.Fakers.Products;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;
using Xunit;

namespace UnitTest.Application.Orders.CancelOrder
{
    public class CancelOrderHandlerTest
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CancelOrderHandler _handler;
        private readonly InMemoryRepositoryFactory _repositoryFactory = InMemoryRepositoryFactory.GetInstance();
        
        public CancelOrderHandlerTest()
        {
            _orderRepository = _repositoryFactory.OrderRepository;
            _handler = new CancelOrderHandler(_orderRepository);
        }

        [Fact]
        public async Task Handle_Should_Cancel_Order()
        {
            var orderId = Guid.NewGuid();
            var products = ProductFaker.GenerateProductList();
            var order = OrderFaker.GenerateOrderAsCreated(orderId, products);
            await _orderRepository.CreateAsync(order);

            var request = new CancelOrderRequest { Id = orderId };

            await _handler.Handle(request, CancellationToken.None);

            var canceledOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(orderId);
            canceledOrder!.IsCanceled.Should().BeTrue();
            canceledOrder.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Canceled);
            canceledOrder.StatusHistory.Should().Contain(x => x.GetStatusEnum() == OrderStatusEnum.Canceled);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Order_Not_Found()
        {
            var orderId = Guid.NewGuid();
            var request = new CancelOrderRequest { Id = orderId };
            await Assert.ThrowsAsync<SalesOrderNotFoundException>(() => _handler.Handle(request, CancellationToken.None));
        }
        
        [Fact]
        public async Task Handle_Should_Not_Cancel_Order_If_It_Is_Already_Canceled()
        {
            var orderId = Guid.NewGuid();
            var products = ProductFaker.GenerateProductList();
            var order = OrderFaker.GenerateOrderAsCreated(orderId, products);
            order.CancelOrder();
            await _orderRepository.CreateAsync(order);

            var request = new CancelOrderRequest { Id = orderId };

            await _handler.Handle(request, CancellationToken.None);

            var canceledOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(orderId);
            canceledOrder!.IsCanceled.Should().BeTrue();
            canceledOrder.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Canceled);
            canceledOrder.StatusHistory.Should().Contain(x => x.GetStatusEnum() == OrderStatusEnum.Canceled);
        }
    }
}