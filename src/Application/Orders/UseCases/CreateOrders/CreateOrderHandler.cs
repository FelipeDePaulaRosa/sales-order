using Domain.Orders;
using Domain.Shared.Contracts;
using MediatR;

namespace Application.Orders.UseCases.CreateOrders;

public class CreateOrderHandler : IRequestHandler<CreateOrderRequest, CreateOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    
    public CreateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }
    
    public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var productsIds = request.Products.Select(x => x.ProductId);
        var products = await _productRepository.GetProductsByIds(productsIds);
        
        var order = new Order(
            request.Number,
            request.SaleDate,
            request.CustomerId,
            request.MerchantId);
        
        request.Products.ForEach(productRequest =>
        {
            var product = products.First(x => x.Id == productRequest.ProductId);
            order.AddProduct(product.Id, productRequest.Quantity, product.GetPrice(), product.GetDiscount());
        });
        
        order.CalcAmount();
        
        var response = await _orderRepository.CreateAsync(order);
        
        return new CreateOrderResponse
        {
            Id = response.Id,
            Number = response.Number,
            SaleDate = response.SaleDate,
            Amount = response.GetAmountValue(),
            Status = response.GetCurrentStatusEnum(),
            CustomerId = response.CustomerId,
            MerchantId = response.MerchantId
        };
    }
}