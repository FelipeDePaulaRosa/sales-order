using Domain.Orders.Entities;

namespace Application.Orders.UseCases.UpdateOrder;

public class UpdateOrderResponse
{
    public Guid Id { get; init; }
    public string Number { get; init; }
    public DateTime SaleDate { get; init; }
    public decimal Amount { get; init; }
    public OrderStatus Status { get; init; }
    public bool IsCanceled { get; init; }
    public Guid CustomerId { get; init; }
    public Guid MerchantId { get; init; }
    public List<UpdateOrderProductResponse> Products { get; init; }

    public UpdateOrderResponse(Order order)
    {
        Id = order.Id;
        Number = order.Number;
        SaleDate = order.SaleDate;
        Amount = order.GetAmountValue();
        Status = order.Status;
        IsCanceled = order.IsCanceled;
        CustomerId = order.CustomerId;
        MerchantId = order.MerchantId;
        Products = order.Products.Select(x => new UpdateOrderProductResponse(x)).ToList();
    }
}

public record UpdateOrderProductResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public bool IsCanceled { get; init; }
    
    public UpdateOrderProductResponse(OrderProduct orderProduct)
    {
        Id = orderProduct.Id;
        ProductId = orderProduct.ProductId;
        Quantity = orderProduct.Quantity;
        IsCanceled = orderProduct.IsCanceled;
    }
}