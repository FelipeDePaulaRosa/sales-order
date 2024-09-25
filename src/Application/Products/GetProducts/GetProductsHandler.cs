using Domain.Shared.Contracts;
using MediatR;

namespace Application.Products.GetProducts;

public class GetProductsHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsNoTrackAsync();
        return new GetProductsResponse(products, request.PageNumber, request.PageSize);
    }
}
