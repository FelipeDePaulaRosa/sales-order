using MediatR;

namespace Application.Orders.UseCases.GetOrderById;

public record GetOrderByIdRequest : IRequest<GetOrderByIdResponse>
{
    public Guid Id { get; init; }
}