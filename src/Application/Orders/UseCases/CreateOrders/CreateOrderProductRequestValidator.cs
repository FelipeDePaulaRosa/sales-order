using Domain.Products;
using Domain.Shared.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders.UseCases.CreateOrders;

public class CreateOrderProductRequestValidator : AbstractValidator<CreateOrderProductRequest>
{
    private readonly IProductRepository _productRepository;
    private Product? Product;
    
    public CreateOrderProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        
        RuleFor(x => x.ProductId)
            .NotNull()
            .Must(ProductExists)
            .WithMessage("Product not found");
        
        When(x => Product is not null, () =>
        {
            //TODO: When Active validate Quantity and Stock
            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage(x => $"Product '{x.ProductId}' must have a quantity greater than 0");
        
            RuleFor(x => x)
                .Must(ProductHasStock)
                .WithMessage(x => $"Product {x.ProductId} out of stock");
            
            RuleFor(x => x)
                .Must(ProductIsActive)
                .WithMessage(x => $"Product {x.ProductId} is inactive");
        });
    }
    
    private bool ProductExists(Guid id)
    {
        Product = _productRepository
            .GetDbSet()
            .AsNoTracking()
            .FirstOrDefault(x => x.Id == id);
        
        return Product is not null;
    }
    
    private bool ProductHasStock(CreateOrderProductRequest request)
        => Product!.Stock >= request.Quantity;

    private bool ProductIsActive(CreateOrderProductRequest request)
        => Product!.Active;

}