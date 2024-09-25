using Application.Orders.UseCases.CreateOrders;
using Domain.Orders.Entities;
using Domain.Products.Entities;
using Domain.Shared.Contracts;
using FluentAssertions;
using UnitTest.TestHelpers.Fakers.Products;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;
using Xunit;

namespace UnitTest.Application.Orders.CreateOrders;

public class CreateOrderHandlerTest
{
    private readonly CreateOrderHandler _handler;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderHandlerTest()
    {
        var inMemoryRepositoryFactory = InMemoryRepositoryFactory.GetInstance();
        _orderRepository = inMemoryRepositoryFactory.OrderRepository;
        _productRepository = inMemoryRepositoryFactory.ProductRepository;
        _handler = new CreateOrderHandler(_orderRepository, _productRepository);
    }

    [Fact]
    public async Task Should_Create_Order_Successfully()
    {
        var products = ProductFaker.GenerateProductList(2);
        await _productRepository.CreateRangeAsync(products);
        var request = GetCreateOrderRequest(products);
        var response = await _handler.Handle(request, CancellationToken.None);

        var createdOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(response.Id);

        createdOrder.Should().NotBeNull();
        if (createdOrder == null) return;
        createdOrder.Number.Should().Be(request.Number);
        createdOrder.SaleDate.Should().Be(request.SaleDate);
        createdOrder.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Created);
        createdOrder.CustomerId.Should().Be(request.CustomerId);
        createdOrder.MerchantId.Should().Be(request.MerchantId);
        createdOrder.Products.Count.Should().Be(request.Products.Count);
        createdOrder.GetAmountValue().Should().Be(createdOrder.Products.Sum(x => x.Amount.GetValueFromCents()));
    }

    private CreateOrderRequest GetCreateOrderRequest(List<Product> products)
        => new CreateOrderRequest
        {
            Number = "000000000000000000X1",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            MerchantId = Guid.NewGuid(),
            Products = new List<CreateOrderProductRequest>
            {
                new()
                {
                    ProductId = products[0].Id,
                    Quantity = 1
                },
                new()
                {
                    ProductId = products[1].Id,
                    Quantity = 2
                }
            }
        };
}