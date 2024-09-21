using Application.Orders.Requests;
using Application.Orders.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class OrderController : ApiController
{
    private readonly IOrderService _orderService;
    private readonly ILogger<ApiController> _logger;

    public OrderController(IOrderService orderService, ILogger<ApiController> logger) : base(logger)
    {
        _orderService = orderService;
        _logger = logger;
    }
    
    public async Task CreateOrder([FromBody] CreateOrderRequest order)
    {
        // Create order
    }
}