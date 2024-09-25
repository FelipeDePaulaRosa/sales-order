using Domain.Orders.Entities;
using Domain.Shared.Exceptions;
using FluentAssertions;
using UnitTest.TestHelpers.Fakers.Orders;
using UnitTest.TestHelpers.Fakers.Products;
using Xunit;

namespace UnitTest.Domain.Orders;

public class OrderTest
{
    [Fact]
    public void MarkStatusAsUpdated_Should_Set_Status_To_Updated()
    {
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), ProductFaker.GenerateProductList());
        order.MarkStatusAsUpdated();

        order.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Updated);
        order.IsCanceled.Should().BeFalse();
        order.StatusHistory.Should().Contain(x => x.GetStatusEnum() == OrderStatusEnum.Updated);
    }
    
    [Fact]
    public void MarkStatusAsCancelled_Should_Set_Status_To_Canceled_And_IsCanceled_To_True()
    {
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), ProductFaker.GenerateProductList());
        order.MarkStatusAsCanceled();

        order.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Canceled);
        order.IsCanceled.Should().BeTrue();
        order.StatusHistory.Should().Contain(x => x.GetStatusEnum() == OrderStatusEnum.Canceled);
    }
    
    [Fact]
    public void Status_Should_Be_Created_When_Order_Was_Created()
    {
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), ProductFaker.GenerateProductList());

        order.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Created);
        order.IsCanceled.Should().BeFalse();
        order.StatusHistory.Should().Contain(x => x.GetStatusEnum() == OrderStatusEnum.Created);
    }
    
    [Fact]
    public void Order_Should_Throw_Exception_When_Try_To_Mark_Status_As_Updated_If_It_Is_Canceled()
    {
        var order = OrderFaker.GenerateOrderAsCreated(Guid.NewGuid(), ProductFaker.GenerateProductList());
        order.MarkStatusAsCanceled();

        order.GetCurrentStatusEnum().Should().Be(OrderStatusEnum.Canceled);
        order.IsCanceled.Should().BeTrue();
        order.StatusHistory.Should().Contain(x => x.GetStatusEnum() == OrderStatusEnum.Canceled);
        
        order.Invoking(x => x.MarkStatusAsUpdated())
            .Should().Throw<SalesOrderApiException>()
            .WithMessage("Cannot change status from Canceled to Updated");
    }
}