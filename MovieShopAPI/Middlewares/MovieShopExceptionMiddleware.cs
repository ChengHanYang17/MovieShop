using System.Net;
using ApplicationCore.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace MovieShopAPI.Middlewares;

public class MovieShopExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<MovieShopExceptionMiddleware> _logger;

    public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            // read info from the HttpContext object and log it somewhere 
            _logger.LogInformation("Inside the Middleware");
            await _next(httpContext);

        }
        catch (Exception ex)
        {
            // first catch the exception
            // check the exception type, message
            // check stacktrace where the exception happened
            // for which URL and which Http method(controller, action method)
            // for which user if user is logged in

            var exceptionDetails = new ErrorModel
            {
                Message = ex.Message, 
                // StackTrace = ex.StackTrace, 
                ExceptionDateTime = DateTime.UtcNow,
                // ExceptionType = ex.GetType().ToString(), 
                Path = httpContext.Request.Path, 
                HttpRequesType = httpContext.Request.Method,
                User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
            };

            // save all this information somewhere, save as text files, json files or database
            // asp.net Code has builtin logging mechanism(ILogger interface) which can be used by any 3rd party log provide such as SeriLog, NLog
            // send email to Dev Team when exception happened 
            _logger.LogError("Exception happened, log this to text or Json files using SeriLog");
            
            // return http status code 500
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize<ErrorModel>(exceptionDetails);
            await httpContext.Response.WriteAsync(result);
            // httpContext.Response.Redirect("/home/error") 
        }
        
        
       
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MovieShopExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<MovieShopExceptionMiddleware>();
    }
} 