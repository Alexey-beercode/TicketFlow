using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using Newtonsoft.Json;
using UserService.BLL.Exceptions;

namespace UserService.Domain.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = context.Response;

        switch (exception)
        {
            case EntityNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            case FormatException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case ValidationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case UnauthorizedAccessException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case AuthenticationException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case AuthorizationException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
            case BadRequestException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                break;
            case AlreadyExistsException:
                response.StatusCode = (int)HttpStatusCode.Conflict;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var exceptionMessage = exception.Message;
        
        var result = JsonConvert.SerializeObject(new 
        {
            error = exceptionMessage,
            details = exception.StackTrace
        });
        
        _logger.Log(LogLevel.Error,result);
        
        return context.Response.WriteAsync(exceptionMessage);
    }
}