using System.Net;
using CrossCutting.FluentValidationNotifications;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ILogger = Serilog.ILogger;

namespace Api.Filters;

public class FluentValidationNotificationFilter : ActionFilterAttribute
{
    private readonly IFluentValidationNotificationContext _notificationContext;
    private readonly ILogger _logger;

    public FluentValidationNotificationFilter(IFluentValidationNotificationContext notificationContext, ILogger logger)
    {
        _notificationContext = notificationContext;
        _logger = logger;
    }

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var apiResponse = new { Errors = _notificationContext.Notifications };
            var responseContent = JsonConvert.SerializeObject(apiResponse);
            _logger.Error("Validation errors: {errors}", responseContent);
            await context.HttpContext.Response.WriteAsync(responseContent);
            return;
        }

        await next();
    }
}