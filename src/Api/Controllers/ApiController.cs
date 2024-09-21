using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    protected readonly ISender Sender;
    protected readonly PathString Path;

    public ApiController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        Sender = sender;
        Path = httpContextAccessor.HttpContext?.Request.Path ?? PathString.Empty;
    }
}