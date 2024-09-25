using Application.Orders.UseCases.UpdateOrder;
using Domain.Shared.Contracts;
using FluentAssertions;
using UnitTest.TestHelpers.Fakers.Orders;
using UnitTest.TestHelpers.Fakers.Products;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;
using Xunit;

namespace UnitTest.Application.Orders.UpdateOrder;

public class UpdateOrderHandlerTest
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly UpdateOrderHandler _handler;
    private readonly InMemoryRepositoryFactory _repositoryFactory = InMemoryRepositoryFactory.GetInstance();

    public UpdateOrderHandlerTest()
    {
        _orderRepository = _repositoryFactory.OrderRepository;
        _productRepository = _repositoryFactory.ProductRepository;
        _handler = new UpdateOrderHandler(_orderRepository, _productRepository);
    }

    [Fact]
    public async Task Should_Update_Order_With_New_Products()
    {
        var productsSeed = ProductFaker.GenerateSpecificCountOfProducts();
        await _productRepository.CreateRangeAsync(productsSeed);

        var products = productsSeed.GetRange(0, 2);
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), products);
        var createdOrder = await _orderRepository.CreateAsync(order);

        var newProducts = productsSeed.GetRange(2, 2);
        var newProductsRequest = newProducts.Select(p => new UpdateOrderProductRequest {Id = null, ProductId = p.Id, Quantity = 1 }).ToList();
        var oldProductsRequest = createdOrder.Products.Select(p => new UpdateOrderProductRequest {Id = p.Id, ProductId = p.ProductId, Quantity = p.Quantity }).ToList();
        var productsRequest = oldProductsRequest.Union(newProductsRequest).ToList();

        var request = new UpdateOrderRequest
        {
            Id = order.Id,
            Number = order.Number,
            SaleDate = order.SaleDate,
            CustomerId = order.CustomerId,
            MerchantId = order.MerchantId,
            Products = productsRequest
        };

        await _handler.Handle(request, CancellationToken.None);

        var updatedOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(order.Id);
        updatedOrder!.Number.Should().Be(request.Number);
        updatedOrder.SaleDate.Should().Be(request.SaleDate);
        updatedOrder.CustomerId.Should().Be(request.CustomerId);
        updatedOrder.MerchantId.Should().Be(request.MerchantId);
        
        productsRequest.Count.Should().Be(updatedOrder.Products.Count);
        foreach (var product in productsRequest)
        {
            updatedOrder.Products.Should().Contain(p => p.ProductId == product.ProductId);
        }
    }
}