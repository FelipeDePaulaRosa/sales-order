using Application.Orders.UseCases.GetOrderById;
using Domain.Shared.Contracts;
using Domain.Shared.Exceptions;
using FluentAssertions;
using UnitTest.TestHelpers.Fakers.Orders;
using UnitTest.TestHelpers.Fakers.Products;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;
using Xunit;

namespace UnitTest.Application.Orders.GetOrderById;

public class GetOrderByIdHandlerTest
{
    private readonly IOrderRepository _orderRepositoryMock;
    private readonly GetOrderByIdHandler _handler;

    public GetOrderByIdHandlerTest()
    {
        _orderRepositoryMock = InMemoryRepositoryFactory.GetInstance().OrderRepository;
        _handler = new GetOrderByIdHandler(_orderRepositoryMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnOrder_WhenOrderExists()
    {
        var orderId = Guid.NewGuid();
        var products = ProductFaker.GenerateSpecificCountOfProducts(3);
        var order = OrderFaker.GenerateOrderAsCreated(orderId, products);

        await _orderRepositoryMock.CreateAsync(order);
        var request = new GetOrderByIdRequest { Id = orderId };

        var result = await _handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(orderId);
        result.Number.Should().Be(order.Number);
        result.SaleDate.Should().Be(order.SaleDate);
        result.Amount.Should().Be(order.GetAmountValue());
        result.Status.Should().BeEquivalentTo(order.Status);
        result.IsCanceled.Should().Be(order.IsCanceled);
        result.CustomerId.Should().Be(order.CustomerId);
        result.MerchantId.Should().Be(order.MerchantId);
        
        result.Products.Count.Should().Be(3);
        result.Products.Should().BeEquivalentTo(order.Products.Select(x => new GetOrderProductByIdResponse(x)));
    }
    
    [Fact]
    public async Task Handle_ShouldThrowSalesOrderNotFoundException_WhenOrderDoesNotExist()
    {
        var request = new GetOrderByIdRequest { Id = Guid.NewGuid() };
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        await act.Should().ThrowAsync<SalesOrderNotFoundException>();
    }
}