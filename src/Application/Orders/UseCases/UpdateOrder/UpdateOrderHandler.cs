using Domain.Orders.Entities;
using Domain.Shared.Contracts;
using MediatR;

namespace Application.Orders.UseCases.UpdateOrder;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderRequest, UpdateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public UpdateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<UpdateOrderResponse> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdAsync(request.Id);
        order.UpdateOrder(request.Number, request.SaleDate, request.CustomerId, request.MerchantId);
        await UpdateOrderProducts(order, request);
        order.CalcAmount();
        await _orderRepository.UpdateAsync(order);
        return new UpdateOrderResponse(order);
    }

    private async Task UpdateOrderProducts(Order order, UpdateOrderRequest request)
    {
        await AddNewProducts(order, request);
        await UpdateExistingProducts(order, request);
    }

    private Task UpdateExistingProducts(Order order, UpdateOrderRequest request)
    {
        var existingProductsRequest = request.Products.Where(x => x.Id is not null).ToList();
        existingProductsRequest.ForEach(productRequest =>
            order.UpdateProduct(productRequest.Id!.Value, productRequest.Quantity, productRequest.IsCanceled)
        );
        return Task.CompletedTask;
    }

    private async Task AddNewProducts(Order order, UpdateOrderRequest request)
    {
        var newProductsRequest = request.Products.Where(x => x.Id is null).ToList();
        if(!newProductsRequest.Any()) return;
        var productsIds = newProductsRequest.Select(x => x.ProductId);
        var products = await _productRepository.GetProductsByIds(productsIds);
        newProductsRequest.ForEach(productRequest =>
        {
            var product = products.First(x => x.Id == productRequest.ProductId);
            order.AddProduct(product.Id, productRequest.Quantity, product.GetPrice(), product.GetDiscount());
        });
    }
}