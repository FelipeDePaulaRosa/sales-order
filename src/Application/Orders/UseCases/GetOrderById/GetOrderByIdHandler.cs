using Domain.Shared.Contracts;
using Domain.Shared.Exceptions;
using MediatR;

namespace Application.Orders.UseCases.GetOrderById;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, GetOrderByIdResponse>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }


    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByIdOrDefaultNoTrackAsync(request.Id);
       
        if(order is null)
            throw new SalesOrderNotFoundException($"Order with id: '{request.Id}' not found");
        
        return new GetOrderByIdResponse(order);
    }
}