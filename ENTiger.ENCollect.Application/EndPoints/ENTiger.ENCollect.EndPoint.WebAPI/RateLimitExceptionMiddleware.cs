using ENCollect.ApiManagement.RateLimiter;

namespace ENTiger.ENCollect;

/// <summary>
/// Intercepts RateLimitExceededException and converts it into a 429 status code
/// which is the right way for server to respond when rate limit is exceeded.
/// </summary>
public class RateLimitExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public RateLimitExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (RateLimitExceededException ex)
        {
            context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}