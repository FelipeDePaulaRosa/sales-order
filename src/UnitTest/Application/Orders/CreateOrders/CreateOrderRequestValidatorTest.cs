using Application.Orders.UseCases.CreateOrders;
using Domain.Products.Entities;
using FluentValidation.TestHelper;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;
using UnitTest.TestHelpers.SeedInjectors.Products;
using Xunit;

namespace UnitTest.Application.Orders.CreateOrders;

public class CreateOrderRequestValidatorTest
{
    private readonly CreateOrderRequestValidator _validator;
    private List<Product> _products;

    public CreateOrderRequestValidatorTest()
    {
        var inMemoryRepository = InMemoryRepositoryFactory.GetInstance();
        var orderRepository = inMemoryRepository.OrderRepository;
        var productRepository = inMemoryRepository.ProductRepository;
        _products = ProductSeedInjector.Inject(productRepository);
        _validator = new CreateOrderRequestValidator(
            orderRepository,
            productRepository);
    }

    [Fact]
    public void Should_not_have_error_when_request_is_valid()
    {
        var model = new CreateOrderRequest
        {
            Number = "000000000000000000X1",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            MerchantId = Guid.NewGuid(),
            Products = new List<CreateOrderProductRequest>
            {
                new()
                {
                    ProductId = _products[0].Id,
                    Quantity = 1
                },
                new()
                {
                    ProductId = _products[1].Id,
                    Quantity = 2
                }
            }
        };
        
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    
    [Fact]
    public void Should_have_error_when_request_is_invalid()
    {
        var model = new CreateOrderRequest
        {
            Number = string.Empty,
            SaleDate = DateTime.UtcNow.AddDays(-1),
        };
        
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Number);
        result.ShouldHaveValidationErrorFor(x => x.SaleDate);
    }
}