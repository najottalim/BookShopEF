using System.Net;
using BookStore.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BookStore.Service.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (HttpStatusCodeException exception)
        {
            await HandleAsync(exception, httpContext);
        }
        catch (Exception error)
        {
            await HandleOtherExceptionAsync(error, httpContext);
        }
    }

    public async Task HandleAsync(HttpStatusCodeException statusCodeException, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = (int)statusCodeException.StatusCode;
        httpContext.Response.ContentType = "application/json";
        var json = JsonConvert.SerializeObject(new
            {StatusCode = statusCodeException.StatusCode, Message = statusCodeException.Message});
        await httpContext.Response.WriteAsync(json);
    }

    public async Task HandleOtherExceptionAsync(Exception error, HttpContext httpContext)
    {
        httpContext.Response.StatusCode = 500;
        httpContext.Response.ContentType = "application/json";
        var json = JsonConvert.SerializeObject(new
            {StatusCode = HttpStatusCode.InternalServerError, Message = error.Message});
        await httpContext.Response.WriteAsync(json);
    }
}