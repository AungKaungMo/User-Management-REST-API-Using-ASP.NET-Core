using Microsoft.AspNetCore.Mvc;

namespace UserManagementApi.Middleware;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled request error");
            if (context.Response.HasStarted) throw;
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await Results.Problem(
                title: "An unexpected server error occurred.",
                statusCode: StatusCodes.Status500InternalServerError
            ).ExecuteAsync(context);
        }
    }
}
