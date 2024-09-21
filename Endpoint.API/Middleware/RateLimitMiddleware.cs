using System.Text.Json;

namespace Endpoint.API.Middleware;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;

    public RateLimitMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
        {
            context.Response.ContentType = "application/json";
            var response = new { message = "You've made too many requests. Please try again later." };
            var jsonResponse = JsonSerializer.Serialize(response);

            context.Response.ContentLength = jsonResponse.Length;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
