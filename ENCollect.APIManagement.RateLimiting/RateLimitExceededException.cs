namespace ENCollect.ApiManagement.RateLimiter;

/// <summary>
/// Thrown when the rate limit has been exceeded for a given API and user.
/// </summary>
public class RateLimitExceededException : Exception
{
    public string Api { get; }
    public string UserId { get; }

    public RateLimitExceededException(string api, string userId, string message)
        : base(message)
    {
        Api = api;
        UserId = userId;
    }
}