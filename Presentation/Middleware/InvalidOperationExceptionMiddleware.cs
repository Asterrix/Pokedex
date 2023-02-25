using System.Net;
using Application;
using Newtonsoft.Json;
using Presentation.Error;

namespace Presentation.Middleware;

public class InvalidOperationExceptionMiddleware : IMiddleware
{
    private readonly ILogger<InvalidOperationExceptionMiddleware> _logger;

    public InvalidOperationExceptionMiddleware(ILogger<InvalidOperationExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var problemDetails = new ErrorModel
            {
                Type = "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.1",
                Title = "Validation Error",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = "Validation error occured"
            };
            problemDetails.ErrorList.Add(e.Message);

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, jsonSettings));
        }
    }
}