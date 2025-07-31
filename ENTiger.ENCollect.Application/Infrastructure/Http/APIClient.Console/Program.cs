using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;

namespace ENTiger.ENCollect
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Create a host for DI and logging.
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // --- DI & Polly Configuration for the API Client ---
                    services.AddHttpClient<IApiHelper, ApiHelper>(client =>
                    {
                        client.Timeout = TimeSpan.FromSeconds(30);
                    })
                    .AddPolicyHandler(GetRetryPolicy())
                    .AddPolicyHandler(GetCircuitBreakerPolicy());

                    // Register the caller class.
                    services.AddTransient<ApiCallerDemo>();
                })
                .Build();

            // Retrieve and run the caller (the actual API calls are trivial).
            var demo = host.Services.GetRequiredService<ApiCallerDemo>();
            await demo.RunAsync();

            // Optionally, run the host to keep background services running.
            await host.RunAsync();
        }

        #region Polly Policies Configuration

        /// <summary>
        /// Defines a retry policy that retries 3 times with exponential backoff for transient HTTP errors.
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        /// <summary>
        /// Defines a circuit breaker policy that breaks the circuit after 5 consecutive errors for 30 seconds.
        /// </summary>
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy() =>
            HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));

        #endregion
    }
}