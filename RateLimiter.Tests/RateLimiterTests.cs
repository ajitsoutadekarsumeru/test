using Microsoft.Extensions.Configuration;

namespace ENCollect.ApiManagement.RateLimiter.Tests
{
    public class RateLimiterTests
    {
        private readonly RateLimiter _rateLimiter;

        public RateLimiterTests()
        {
            // Build an in-memory IConfiguration for testing:
            // You can simulate "appsettings.json" by constructing a dictionary.
            var inMemorySettings = new Dictionary<string, string>
            {
                // Key format for Configuration: "RateLimitSettings:0:Api"
                { "RateLimitSettings:0:Api", "issue_receipt" },
                { "RateLimitSettings:0:PermitLimit", "3" },
                { "RateLimitSettings:0:WindowInSeconds", "10" },

                { "RateLimitSettings:1:Api", "some_other_api" },
                { "RateLimitSettings:1:PermitLimit", "2" },
                { "RateLimitSettings:1:WindowInSeconds", "2" },
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _rateLimiter = new RateLimiter(configuration);
        }

        [Fact]
        public void ApiNotInConfig_NoRateLimit_ExpectNoException()
        {
            // scenario 1: "unknown_api" is not in config, so no limit is enforced.
            for (int i = 0; i < 100; i++)
            {
                _rateLimiter.EnforceRateLimit("unknown_api", "bob");
            }

            // If we reach here, no exception was thrown => pass
        }

        [Fact]
        public void WithinPermitLimit_ShouldNotThrow()
        {
            // scenario 2: for "issue_receipt", config => PermitLimit=3, window=10s
            // calling 3 times quickly => no exception
            _rateLimiter.EnforceRateLimit("issue_receipt", "bob");
            _rateLimiter.EnforceRateLimit("issue_receipt", "bob");
            _rateLimiter.EnforceRateLimit("issue_receipt", "bob");

            // pass if no exception
        }

        [Fact]
        public void ExceedPermitLimit_ShouldThrowRateLimitExceededException()
        {
            // scenario 3: we said above the limit is 3 per 10s for "issue_receipt"
            _rateLimiter.EnforceRateLimit("issue_receipt", "alice");
            _rateLimiter.EnforceRateLimit("issue_receipt", "alice");
            _rateLimiter.EnforceRateLimit("issue_receipt", "alice");

            // The 4th call should throw
            Assert.Throws<RateLimitExceededException>(() =>
            {
                _rateLimiter.EnforceRateLimit("issue_receipt", "alice");
            });
        }

        [Fact]
        public async Task WindowExpiryResetsCount_ShouldAllowAfterWait()
        {
            // scenario 4: for "some_other_api", PermitLimit=2, WindowInSeconds=2 (from the inMemory config)
            // We'll do 2 calls quickly => no exception
            _rateLimiter.EnforceRateLimit("some_other_api", "bob");
            _rateLimiter.EnforceRateLimit("some_other_api", "bob");

            // immediate 3rd => should throw
            Assert.Throws<RateLimitExceededException>(() =>
            {
                _rateLimiter.EnforceRateLimit("some_other_api", "bob");
            });

            // Wait for 2+ seconds so the window expires
            await Task.Delay(TimeSpan.FromSeconds(3));

            // Now we should be in a new window, so the next call is allowed
            _rateLimiter.EnforceRateLimit("some_other_api", "bob");
            // pass => no exception
        }

        [Fact]
        public void NullOrEmptyInputs_ShouldNotThrow()
        {
            // scenario 5: If string.IsNullOrWhiteSpace(api) or userId => no action, no throw by default
            _rateLimiter.EnforceRateLimit(null, "bob");        // no exception
            _rateLimiter.EnforceRateLimit("", "bob");          // no exception
            _rateLimiter.EnforceRateLimit("issue_receipt", ""); // no exception
            _rateLimiter.EnforceRateLimit("issue_receipt", null); // no exception

            // pass => no exception
        }

        [Fact]
        public void ConcurrencyTest_BasicCheck()
        {
            // scenario 6 (simple version):
            // "issue_receipt" => limit=3 per 10s
            // We'll run 5 tasks simultaneously => expect 3 successes, 2 throw

            var tasks = new List<Task>();
            int successCount = 0;
            int failCount = 0;

            for (int i = 0; i < 5; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    try
                    {
                        _rateLimiter.EnforceRateLimit("issue_receipt", "charlie");
                        lock (tasks)
                        {
                            successCount++;
                        }
                    }
                    catch (RateLimitExceededException)
                    {
                        lock (tasks)
                        {
                            failCount++;
                        }
                    }
                }));
            }

            Task.WaitAll(tasks.ToArray());

            // We expect exactly 3 successes, 2 fails
            Assert.Equal(3, successCount);
            Assert.Equal(2, failCount);
        }
    }
}