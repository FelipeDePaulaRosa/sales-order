using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    protected readonly ILogger<ApiController> Logger;

    public ApiController(ILogger<ApiController> logger)
    {
        Logger = logger;
    }
}