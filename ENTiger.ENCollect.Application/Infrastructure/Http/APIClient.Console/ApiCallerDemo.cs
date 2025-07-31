

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Demonstrates API calls using the ApiClient library.
    /// Illustrates both JSON POST and FormUrlEncoded POST.
    /// </summary>
    public class ApiCallerDemo
    {
        private readonly IApiHelper _apiHelper;

        public ApiCallerDemo(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// Makes two API calls: one using JSON and one using FormUrlEncodedContent.
        /// In case of failure, publishes a DomainFailureEvent.
        /// </summary>
        public async Task RunAsync()
        {
            string apiUrl = "https://httpbin.org/post";  // Demo endpoint

            // --- JSON POST Call ---
            string jsonPayload = "{\"message\":\"Hello, JSON!\"}";
            try
            {
                var result = await _apiHelper.SendRequestAsync(jsonPayload, apiUrl);
                // Extract the response content as string
                string jsonResponse = await result.Content.ReadAsStringAsync();
                Console.WriteLine("JSON API Response:");
                Console.WriteLine(jsonResponse);
            }
            catch (Exception ex)
            {
                var failureEvent = new DomainFailureEvent
                {
                    ApiUrl = apiUrl,
                    Payload = jsonPayload,
                    ErrorMessage = ex.Message,
                    Timestamp = DateTime.UtcNow
                };
                PublishDomainFailureEvent(failureEvent);
            }

            // --- FormUrlEncoded POST Call ---
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", "testuser"),
                new KeyValuePair<string, string>("password", "secret")
            };
            try
            {
                var result = await _apiHelper.SendFormUrlEncodedRequestAsync(formData, apiUrl);
                // Extract the response content as string
                string formResponse = await result.Content.ReadAsStringAsync();
                Console.WriteLine("FormUrlEncoded API Response:");
                Console.WriteLine(formResponse);
            }
            catch (Exception ex)
            {
                var failureEvent = new DomainFailureEvent
                {
                    ApiUrl = apiUrl,
                    Payload = "username=testuser, password=secret",
                    ErrorMessage = ex.Message,
                    Timestamp = DateTime.UtcNow
                };
                PublishDomainFailureEvent(failureEvent);
            }
        }

        /// <summary>
        /// Simulates publishing a domain failure event.
        /// In production, this would be sent via an event bus (e.g., NServiceBus).
        /// </summary>
        /// <param name="failureEvent">The event to publish.</param>
        private void PublishDomainFailureEvent(DomainFailureEvent failureEvent)
        {
            Console.WriteLine("Publishing Domain Failure Event:");
            Console.WriteLine(failureEvent);
        }
    }
}