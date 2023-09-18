namespace Eclo.WebApi.middlewares;

public class CrossOriginAccessMiddleware
{
    private readonly RequestDelegate _next;

    public CrossOriginAccessMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
    {
        context.Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
        await _next(context);
    }
}
