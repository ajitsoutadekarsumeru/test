namespace ENCollect.ApiManagement.RateLimiter;

/// <summary>
/// Represents rate-limit settings for a particular API.
/// </summary>
public class ApiRateLimitSetting
{
    public string Api { get; set; } = string.Empty;

    /// <summary>
    /// Max requests a user can make within the time window.
    /// </summary>
    public int PermitLimit { get; set; }

    /// <summary>
    /// Size of the window in seconds for this API.
    /// </summary>
    public double WindowInSeconds { get; set; }
}