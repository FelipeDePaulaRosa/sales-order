using Application.Orders.UseCases.CreateOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrderController : ApiController
{
    public OrderController(ISender sender, IHttpContextAccessor httpContextAccessor) 
        : base(sender, httpContextAccessor)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var response = await Sender.Send(request);
        return Created(Path, response);
    }
}