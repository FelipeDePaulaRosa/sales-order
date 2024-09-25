using CrossCutting.Utils;
using MediatR;

namespace Application.Products.GetProducts;

public class GetProductsRequest : PagedQueryRequest, IRequest<GetProductsResponse>
{
    public GetProductsRequest(int pageNumber, int pageSize)
        : base(pageNumber, pageSize)
    {
    }
}