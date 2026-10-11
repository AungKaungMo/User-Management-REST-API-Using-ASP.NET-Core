using System.Diagnostics;

namespace UserManagementApi.Middleware;

public sealed class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var timer = Stopwatch.StartNew();
        try
        {
            await next(context);
        }
        finally
        {
            timer.Stop();
            // Do not log query strings, request bodies, or passwords.
            logger.LogInformation("HTTP {Method} {Path} => {StatusCode} ({ElapsedMs} ms)",
                context.Request.Method, context.Request.Path.Value,
                context.Response.StatusCode, timer.ElapsedMilliseconds);
        }
    }
}
