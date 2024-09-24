using Domain.Orders.Entities;

namespace Application.Orders.UseCases.GetOrderById;

public record GetOrderByIdResponse
{
    public Guid Id { get; init; }
    public string Number { get; init; }
    public DateTime SaleDate { get; init; }
    public decimal Amount { get; init; }
    public OrderStatus Status { get; init; }
    public bool IsCanceled { get; init; }
    public Guid CustomerId { get; init; }
    public Guid MerchantId { get; init; }
    public List<GetOrderProductByIdResponse> Products { get; init; }

    public GetOrderByIdResponse(Order order)
    {
        Id = order.Id;
        Number = order.Number;
        SaleDate = order.SaleDate;
        Amount = order.GetAmountValue();
        Status = order.Status;
        IsCanceled = order.IsCanceled;
        CustomerId = order.CustomerId;
        MerchantId = order.MerchantId;
        Products = order.Products.Select(x => new GetOrderProductByIdResponse(x)).ToList();
    }
    
}

public record GetOrderProductByIdResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public bool IsCanceled { get; set; }
    
    public GetOrderProductByIdResponse(OrderProduct orderProduct)
    {
        Id = orderProduct.Id;
        ProductId = orderProduct.ProductId;
        Quantity = orderProduct.Quantity;
        IsCanceled = orderProduct.IsCanceled;
    }
}