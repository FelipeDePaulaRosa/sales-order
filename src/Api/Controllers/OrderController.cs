using Application.Orders.UseCases.CreateOrders;
using Application.Orders.UseCases.GetOrderById;
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
    {
        var response = await Sender.Send(new GetOrderByIdRequest { Id = id });
        return Ok(response);
    }
}