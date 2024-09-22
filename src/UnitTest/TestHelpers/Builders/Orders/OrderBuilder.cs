using Domain.Orders;

namespace UnitTest.TestHelpers.Builders.Orders;

public class OrderBuilder
{
    private string _number = "123";
    private DateTime _saleDate = DateTime.UtcNow;
    private decimal _amount = 100.00m;
    private Guid _customerId = Guid.NewGuid();
    private Guid _merchantId = Guid.NewGuid();

    public OrderBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public OrderBuilder WithSaleDate(DateTime saleDate)
    {
        _saleDate = saleDate;
        return this;
    }

    public OrderBuilder WithAmount(decimal amount)
    {
        _amount = amount;
        return this;
    }

    public OrderBuilder WithCustomerId(Guid customerId)
    {
        _customerId = customerId;
        return this;
    }

    public OrderBuilder WithMerchantId(Guid merchantId)
    {
        _merchantId = merchantId;
        return this;
    }

    public Order Build()
    {
        return new Order(_number, _saleDate, _amount, _customerId, _merchantId);
    }
}