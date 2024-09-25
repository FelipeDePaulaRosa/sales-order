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
        var productsSeed = ProductFaker.GenerateProductList();
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
    
    [Fact]
    public async Task Should_AddStockEvent_When_ProductIsCanceled()
    {
        var products = ProductFaker.GenerateProductList(1);
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), products);
        order = await _orderRepository.CreateAsync(order);

        var productRequest = new UpdateOrderProductRequest
        {
            Id = order.Products.First().Id,
            ProductId = order.Products.First().Id,
            Quantity = order.Products.First().Quantity,
            IsCanceled = true
        };

        var request = new UpdateOrderRequest
        {
            Id = order.Id,
            Products = new List<UpdateOrderProductRequest> { productRequest }
        };

        await _handler.Handle(request, CancellationToken.None);

        var updatedOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(order.Id);
        var updatedProduct = updatedOrder.Products.First();
        updatedProduct.IsCanceled.Should().BeTrue();
    }
    
    [Fact]
    public async Task Should_RemoveStockEvent_When_ProductIsUncanceled()
    {
        var products = ProductFaker.GenerateProductList(1);
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), products);
        var product = order.Products.First();
        order.UpdateProduct(order.Products.First().Id, product.Quantity, true);
        await _orderRepository.CreateAsync(order);

        var productRequest = new UpdateOrderProductRequest
        {
            Id = order.Products.First().Id,
            ProductId = product.Id,
            Quantity = product.Quantity,
            IsCanceled = false
        };

        var request = new UpdateOrderRequest
        {
            Id = order.Id,
            Products = new List<UpdateOrderProductRequest> { productRequest }
        };

        await _handler.Handle(request, CancellationToken.None);

        var updatedOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(order.Id);
        var updatedProduct = updatedOrder.Products.First();
        updatedProduct.IsCanceled.Should().BeFalse();
    }
    
    [Fact]
    public async Task Should_RemoveStockEvent_When_QuantityIncreases()
    {
        var products = ProductFaker.GenerateProductList(1);
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), products);
        await _orderRepository.CreateAsync(order);
    
        var product = order.Products.First();
        var newQuantity = product.Quantity + 5;
        var productRequest = new UpdateOrderProductRequest
        {
            Id = order.Products.First().Id,
            ProductId = product.Id,
            Quantity = newQuantity,
            IsCanceled = false
        };
    
        var request = new UpdateOrderRequest
        {
            Id = order.Id,
            Products = new List<UpdateOrderProductRequest> { productRequest }
        };
    
        await _handler.Handle(request, CancellationToken.None);
    
        var updatedOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(order.Id);
        var updatedProduct = updatedOrder.Products.First();
        updatedProduct.Quantity.Should().Be(newQuantity);
    }
    
    [Fact]
    public async Task Should_AddStockEvent_When_QuantityDecreases()
    {
        var products = ProductFaker.GenerateProductList(1);
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), products);
        await _orderRepository.CreateAsync(order);
    
        var product = order.Products.First();
        var newQuantity = product.Quantity - 5;
        var productRequest = new UpdateOrderProductRequest
        {
            Id = order.Products.First().Id,
            ProductId = product.Id,
            Quantity = newQuantity,
            IsCanceled = false
        };
    
        var request = new UpdateOrderRequest
        {
            Id = order.Id,
            Products = new List<UpdateOrderProductRequest> { productRequest }
        };
    
        await _handler.Handle(request, CancellationToken.None);
    
        var updatedOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(order.Id);
        var updatedProduct = updatedOrder.Products.First();
        updatedProduct.Quantity.Should().Be(newQuantity);
    }
    
    [Fact]
    public async Task Should_NotTriggerEvents_When_QuantityIsSame()
    {
        var products = ProductFaker.GenerateProductList(1);
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), products);
        await _orderRepository.CreateAsync(order);
    
        var product = order.Products.First();
        var productRequest = new UpdateOrderProductRequest
        {
            Id = order.Products.First().Id,
            ProductId = product.Id,
            Quantity = product.Quantity,
            IsCanceled = false
        };
    
        var request = new UpdateOrderRequest
        {
            Id = order.Id,
            Products = new List<UpdateOrderProductRequest> { productRequest }
        };
    
        await _handler.Handle(request, CancellationToken.None);
    
        var updatedOrder = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(order.Id);
        var updatedProduct = updatedOrder.Products.First();
        updatedProduct.Quantity.Should().Be(product.Quantity);
    }
}