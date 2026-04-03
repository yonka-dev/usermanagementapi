using System.Diagnostics;

namespace UserManagementAPI.Middleware;

public class RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Log incoming request
        _logger.LogInformation("Incoming Request: {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

        await _next(context);

        stopwatch.Stop();

        // Log outgoing response
        _logger.LogInformation("Outgoing Response: {StatusCode} for {Method} {Path} in {Elapsed} ms",
            context.Response.StatusCode,
            context.Request.Method,
            context.Request.Path,
            stopwatch.ElapsedMilliseconds);
    }
}