using System.Net;
using Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ILogger = Serilog.ILogger;

namespace Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger _logger;

    public ExceptionFilter(ILogger logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.Error(context.Exception, "Unhandled exception occurred on {RequestPath}", context.HttpContext.Request.Path);

        var exception = context.Exception;
        
        context.Result = new ObjectResult(new
        {
            Error = exception.Message
        })
        {
            StatusCode = exception switch
            {
                SalesOrderApiException => (int)HttpStatusCode.BadRequest,
                SalesOrderNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            }
        };

        context.ExceptionHandled = true;
    }
}