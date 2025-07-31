using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Provides a helper to send HTTP POST requests with either JSON or form-encoded payloads.
    /// </summary>
    /// <remarks>
    /// This class relies on an HttpClient that may have Polly policies configured via dependency injection (DI).
    /// </remarks>
    public class ApiHelper : IApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiHelper> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiHelper"/> class.
        /// </summary>
        /// <param name="httpClient">An HttpClient instance injected via DI.</param>
        /// <param name="logger">An ILogger instance for logging, injected via DI.</param>
        public ApiHelper(HttpClient httpClient, ILogger<ApiHelper> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

       
        /// <inheritdoc />
        public async Task<HttpResponseMessage> SendRequestAsync(
            string? json,
            string apiUrl,
            HttpMethod method,
            Dictionary<string, string> headers = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                DateTime startTime = DateTime.Now;

                _logger.LogInformation("Sending JSON request to {ApiUrl}", apiUrl);

                using var content = json != null ? new StringContent(json, Encoding.UTF8, "application/json") : null;
                using var request = new HttpRequestMessage(method, apiUrl) { Content = content };
                AddHeaders(request, headers);
                var response = await _httpClient.SendAsync(request, cancellationToken);
                
                _logger.LogInformation("Received response from {ApiUrl} with status {StatusCode}", apiUrl, response.StatusCode);

                _logger.LogInformation("API processing time: {processingtime} for {ApiUrl} with {data}", (DateTime.Now - startTime).ToString(), apiUrl,json);
                return response;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "JSON request failed to {ApiUrl}", apiUrl);
                throw;
            }
        }

        public async Task<HttpResponseMessage> SendFormUrlEncodedRequestAsync(
        IEnumerable<KeyValuePair<string, string>> formData,
        string apiUrl,
        HttpMethod method,
        Dictionary<string, string> headers = null,
        CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Sending FormUrlEncoded request to {ApiUrl}", apiUrl);

                using var content = new FormUrlEncodedContent(formData);
                using var request = new HttpRequestMessage(method, apiUrl) { Content = content };

                AddHeaders(request, headers);

                 var response = await _httpClient.SendAsync(request, cancellationToken);
                _logger.LogInformation("Received response from {ApiUrl} with status {StatusCode}", apiUrl, response.StatusCode);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FormUrlEncoded request failed to {ApiUrl}", apiUrl);
                throw;
            }
        }

        private static void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            if (headers == null) return;

            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                if (header.Key != null && header.Key.ToLower().Contains("bearer"))
                    request.Headers.Authorization = new AuthenticationHeaderValue(header.Key, header.Value);
            }
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response, string apiUrl, CancellationToken cancellationToken)
        {
            var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Received response from {ApiUrl}: {Response}", apiUrl, responseString);
                return JsonSerializer.Deserialize<T>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            _logger.LogWarning("Request to {ApiUrl} failed with status {StatusCode}: {Response}", apiUrl, response.StatusCode, responseString);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("Unauthorized: Invalid authentication credentials.");

            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new HttpRequestException($"Bad request: {responseString}");

            if (response.StatusCode == HttpStatusCode.InternalServerError)
                throw new HttpRequestException($"Server error: {responseString}");

            response.EnsureSuccessStatusCode();
            return default;
        }

    }
}