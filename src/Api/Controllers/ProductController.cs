using Application.Products.GetProducts;
using CrossCutting.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductController : ApiController
{
    public ProductController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }
    
    [HttpGet]
    public async Task<ActionResult> GetProducts([FromQuery] QueryPagedParameters parameters)
    {
        var request = new GetProductsRequest(parameters.PageNumber, parameters.PageSize);
        var response = await Sender.Send(request);
        return Ok(response);
    }
}