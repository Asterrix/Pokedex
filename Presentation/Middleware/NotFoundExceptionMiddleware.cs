using System.Net;
using Application.Contracts;
using Newtonsoft.Json;
using Presentation.Error;

namespace Presentation.Middleware;

public class NotFoundExceptionMiddleware : IMiddleware
{
    private readonly ILogger<NotFoundExceptionMiddleware> _logger;

    public NotFoundExceptionMiddleware(ILogger<NotFoundExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var problemDetails = new ErrorModel
            {
                Type = "https://www.rfc-editor.org/rfc/rfc9110#section-15.5.5",
                Title = "Not Found Error",
                Status = (int)HttpStatusCode.NotFound,
                Detail = "Not found error occured"
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