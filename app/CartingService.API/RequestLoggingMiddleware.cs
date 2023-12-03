using Microsoft.Net.Http.Headers;

namespace CartingService.API;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path} {context.Request.Headers[HeaderNames.Authorization]}");
        await _next(context);
    }
}