using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Defines a contract for sending HTTP POST requests with different types of content.
    /// </summary>
    public interface IApiHelper
    {
        /// <summary>
        /// Sends a JSON payload as an HTTP POST request to the specified URL.
        /// </summary>
        /// <param name="json">The JSON payload to send.</param>
        /// <param name="apiUrl">The URL of the API endpoint.</param>
        /// <param name="idempotencyKey">
        /// An optional idempotency key. If provided, it will be included as the "Idempotency-Key" header.
        /// </param>
        /// <param name="authToken">
        /// An optional authentication token. If provided, it will be included as the "Authorization" header using the Bearer scheme.
        /// It is assumed that the token is already valid and the authentication process is handled separately.
        /// </param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the response as a string.
        /// </returns>
        /// <remarks>
        /// If Polly policies are configured via the DI container, they will automatically apply to the underlying HttpClient.
        /// </remarks>
        Task<HttpResponseMessage> SendRequestAsync(
               string? json,
               string apiUrl,
               HttpMethod method,
               Dictionary<string, string> headers = null,
               CancellationToken cancellationToken = default);

        /// <summary>
        /// Sends form-encoded data as an HTTP POST request to the specified URL.
        /// </summary>
        /// <param name="formData">
        /// The form data as a collection of key/value pairs.
        /// </param>
        /// <param name="apiUrl">The URL of the API endpoint.</param>
        /// <param name="idempotencyKey">
        /// An optional idempotency key. If provided, it will be included as the "Idempotency-Key" header.
        /// </param>
        /// <param name="authToken">
        /// An optional authentication token. If provided, it will be included as the "Authorization" header using the Bearer scheme.
        /// It is assumed that the token is already valid and the authentication process is handled separately.
        /// </param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// A task that represents the asynchronous operation, containing the response as a string.
        /// </returns>

        Task<HttpResponseMessage> SendFormUrlEncodedRequestAsync(
        IEnumerable<KeyValuePair<string, string>> formData,
        string apiUrl,
        HttpMethod method, 
        Dictionary<string, string> headers = null,
        CancellationToken cancellationToken = default);
    }
}