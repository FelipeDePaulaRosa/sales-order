using CrossCutting.Extensions;
using CrossCutting.Utils;
using Domain.Products.Entities;

namespace Application.Products.GetProducts;

public class GetProductsResponse
{
    public PagedListResponse<ProductResponse> Products { get; set; }

    public GetProductsResponse(List<Product> products, int pageNumber, int pageSize)
    {
        Products = products.Select(p => new ProductResponse
        {
            Id = p.Id,
            Code = p.Code,
            Name = p.Name,
            UnitPrice = p.GetPrice(),
            Discount = p.GetDiscount(),
            Brand = p.Brand,
            Stock = p.Stock
        }).ToList().ToPaged(pageNumber, pageSize);
    }
}

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public string Brand { get; set; }
    public int Stock { get; set; }
}