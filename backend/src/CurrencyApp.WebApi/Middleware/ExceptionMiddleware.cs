using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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
        catch (OperationCanceledException) when (ctx.RequestAborted.IsCancellationRequested)
        {
            ctx.Response.StatusCode = 499;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.GatewayTimeout)
        {
            await WriteProblem(ctx, (int)HttpStatusCode.GatewayTimeout, "gateway_timeout", ex);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.BadGateway)
        {
            await WriteProblem(ctx, (int)HttpStatusCode.BadGateway, "bad_gateway", ex);
        }
        catch (HttpRequestException ex)
        {
            await WriteProblem(ctx, (int)HttpStatusCode.BadGateway, "bad_gateway", ex);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled");
            await WriteProblem(ctx, (int)HttpStatusCode.InternalServerError, "internal_error", ex);
        }
    }

    private static async Task WriteProblem(HttpContext ctx, int status, string code, Exception? ex = null)
    {
        if (!ctx.Response.HasStarted)
        {
            ctx.Response.StatusCode = status;
            ctx.Response.ContentType = "application/problem+json";
        }

        var problem = new ProblemDetails
        {
            Status = status,
            Title = code,
            Detail = ex?.Message,
            Instance = ctx.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(problem);
        await ctx.Response.WriteAsync(json);
    }
}
