using System.Net;
using CrossCutting.FluentValidationNotifications;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Api.Filters;

public class FluentValidationNotificationFilter : ActionFilterAttribute
{
    private readonly IFluentValidationNotificationContext _notificationContext;

    public FluentValidationNotificationFilter(IFluentValidationNotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var apiResponse = new { Errors = _notificationContext.Notifications };
            var responseContent = JsonConvert.SerializeObject(apiResponse);
            await context.HttpContext.Response.WriteAsync(responseContent);
            return;
        }

        await next();
    }
}