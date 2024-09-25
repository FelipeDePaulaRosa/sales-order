using Domain.Orders.Entities;
using Domain.Products.Entities;

namespace UnitTest.TestHelpers.Builders.Orders;

public class OrderBuilder
{
    private string _number = "123";
    private DateTime _saleDate = DateTime.UtcNow;
    private Guid _customerId = Guid.NewGuid();
    private Guid _merchantId = Guid.NewGuid();
    private List<OrderProduct> _products = new();

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
    
    public OrderBuilder WithProducts(List<Dictionary<Product, int>> products)
    {
        products.ForEach(x =>
        {
            var product = x.Keys.First();
            var quantity = x.Values.First();
            _products.Add(new OrderProduct(product.Id, quantity, product.GetPrice(), product.GetDiscount()));
        });
        
        return this;
    }

    public Order Build()
    {
        Order order = new(_number, _saleDate, _customerId, _merchantId);
        _products.ForEach(x => order.AddProduct(x.ProductId, x.Quantity, x.UnitPrice.GetValueFromCents(), x.Discount.ValueInPercentage));
        return order;
    }
}