using Eclo.Application.Exceptions;
using Newtonsoft.Json;

namespace Eclo.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IWebHostEnvironment env)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ClientException exception)
        {
            var obj = new
            {
                StatusCode = (int)exception.StatusCode,
                ErrorMessage = exception.TitleMessage,
            };

            httpContext.Response.StatusCode = (int)exception.StatusCode;
            httpContext.Response.Headers.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(obj);
            await httpContext.Response.WriteAsync(json);
        }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/text";
            if (env.IsDevelopment())
            {
                await httpContext.Response.WriteAsync(exception.Message);
            }
            else if (env.IsProduction())
            {
                await httpContext.Response.WriteAsync("There is unknown error!");
            }
        }

    }
}
