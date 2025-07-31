namespace ENCollect.ApiManagement.RateLimiter;

public interface IRateLimiter
{
    /// <summary>
    /// Checks rate limit for the specified API and user.
    /// Throws <see cref="RateLimitExceededException"/> if exceeded.
    /// </summary>
    void EnforceRateLimit(string api, string userId);
}