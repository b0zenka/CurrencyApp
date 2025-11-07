using System.Net;
using Serilog;

namespace CurrencyApp.WebApi.Middleware;
public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext ctx, RequestDelegate next)
    {
        try
        {
            await next(ctx);
        }
        catch (OperationCanceledException)
        {
            ctx.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled");
            ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await ctx.Response.WriteAsJsonAsync(new { error = "internal_error" });
        }
    }
}
