using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;

namespace ENCollect.ApiManagement.RateLimiter;

/// <summary>
/// Simple fixed-window rate limiter that enforces limits per (API, user) combination.
/// </summary>
public class RateLimiter : IRateLimiter
{
    private readonly ConcurrentDictionary<(string Api, string UserId), ApiWindowTracker> _trackerMap
        = new ConcurrentDictionary<(string Api, string UserId), ApiWindowTracker>();

    private readonly Dictionary<string, ApiRateLimitSetting> _configMap
        = new Dictionary<string, ApiRateLimitSetting>(StringComparer.OrdinalIgnoreCase);

    public RateLimiter(IConfiguration configuration)
    {
        // 1. Bind RateLimitSettings from appsettings.json once at startup.
        var rateLimitConfigs = configuration
            .GetSection("RateLimitSettings")
            .Get<List<ApiRateLimitSetting>>() ?? new List<ApiRateLimitSetting>();

        // 2. Build an in-memory map (Api -> Config) for quick lookups.
        foreach (var cfg in rateLimitConfigs)
        {
            if (!string.IsNullOrWhiteSpace(cfg.Api))
            {
                _configMap[cfg.Api] = cfg;
            }
        }
    }

    /// <summary>
    /// Enforce the rate limit for the given API and user. Throws if exceeded.
    /// </summary>
    public void EnforceRateLimit(string api, string userId)
    {
        // 1) Validate the inputs (return or throw if invalid).
        if (!IsValidInput(api, userId))
        {
            return; // or throw, depending on design preference
        }

        // 2) Find rate-limit config for this API. If not found => no limit enforced.
        if (!TryGetRateLimitSetting(api, out var setting))
        {
            return;
        }

        // 3) Retrieve (or create) usage tracker for this (API, user).
        var tracker = GetOrCreateTracker(api, userId);

        // 4) Update usage & check if limit is exceeded.
        CheckAndUpdateTracker(tracker, setting, api, userId);
    }

    #region Private Helpers

    private bool IsValidInput(string api, string userId)
    {
        if (string.IsNullOrWhiteSpace(api) || string.IsNullOrWhiteSpace(userId))
        {
            // If invalid, we choose to do nothing here (or optionally throw).
            return false;
        }
        return true;
    }

    private bool TryGetRateLimitSetting(string api, out ApiRateLimitSetting setting)
    {
        return _configMap.TryGetValue(api, out setting);
    }

    private ApiWindowTracker GetOrCreateTracker(string api, string userId)
    {
        var now = DateTime.UtcNow;
        return _trackerMap.GetOrAdd((api, userId),
            _ => new ApiWindowTracker
            {
                WindowStart = now,
                Count = 0
            });
    }

    private void CheckAndUpdateTracker(
        ApiWindowTracker tracker,
        ApiRateLimitSetting setting,
        string api,
        string userId
    )
    {
        var now = DateTime.UtcNow;
        var windowSize = TimeSpan.FromSeconds(setting.WindowInSeconds);

        lock (tracker.LockObj)
        {
            if (IsWithinWindow(now, tracker.WindowStart, windowSize))
            {
                // We remain in the same window. Increment and check.
                IncrementTrackerCount(tracker, setting, api, userId, windowSize.TotalSeconds);
            }
            else
            {
                // Start a new window.
                ResetTracker(tracker, now);
            }
        }
    }

    private bool IsWithinWindow(DateTime now, DateTime windowStart, TimeSpan windowSize)
    {
        return (now - windowStart) < windowSize;
    }

    private void IncrementTrackerCount(
        ApiWindowTracker tracker,
        ApiRateLimitSetting setting,
        string api,
        string userId,
        double windowSeconds
    )
    {
        tracker.Count++;
        if (tracker.Count > setting.PermitLimit)
        {
            throw new RateLimitExceededException(
                api,
                userId,
                $"Rate limit exceeded for user '{userId}' on API '{api}'. " +
                $"Allowed {setting.PermitLimit} calls per {windowSeconds} seconds."
            );
        }
    }

    private void ResetTracker(ApiWindowTracker tracker, DateTime now)
    {
        tracker.WindowStart = now;
        tracker.Count = 1; // The current request is the first in the new window.
    }

    #endregion Private Helpers

    #region Nested Classes

    private class ApiWindowTracker
    {
        public DateTime WindowStart { get; set; }
        public int Count { get; set; }
        public object LockObj { get; } = new object();
    }

    #endregion Nested Classes
}