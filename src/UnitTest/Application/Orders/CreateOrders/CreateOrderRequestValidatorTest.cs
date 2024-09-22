﻿using Application.Orders.UseCases.CreateOrders;
using FluentValidation.TestHelper;
using UnitTest.TestHelpers.InMemoryDatabaseHelpers;
using Xunit;

namespace UnitTest.Application.Orders.CreateOrders;

public class CreateOrderRequestValidatorTest
{
    private readonly CreateOrderRequestValidator _validator;

    public CreateOrderRequestValidatorTest()
    {
        var inMemoryRepository = InMemoryRepositoryFactory.GetInstance();
        _validator = new CreateOrderRequestValidator(inMemoryRepository.OrderRepository);
    }

    [Fact]
    public void Should_not_have_error_when_request_is_valid()
    {
        var model = new CreateOrderRequest
        {
            Amount = 2.5m,
            Number = "000000000000000000X1",
            SaleDate = DateTime.UtcNow,
            CustomerId = Guid.NewGuid(),
            MerchantId = Guid.NewGuid()
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
        result.ShouldHaveValidationErrorFor(x => x.Amount);
    }
}