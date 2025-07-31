using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

using Microsoft.Extensions.Logging;
using Xunit;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// A custom HttpMessageHandler that captures the outgoing HttpRequestMessage and returns a predefined response.
    /// </summary>
    public class CapturingHttpMessageHandler : HttpMessageHandler
    {
        public HttpRequestMessage CapturedRequest { get; private set; }
        private readonly HttpResponseMessage _response;

        public CapturingHttpMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            CapturedRequest = request;
            return Task.FromResult(_response);
        }
    }

    /// <summary>
    /// A simple stub HttpMessageHandler for tests that do not require request inspection.
    /// </summary>
    public class HttpMessageHandlerStub : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public HttpMessageHandlerStub(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }

    public class ApiHelperTests
    {
        /// <summary>
        /// Creates an ApiHelper using a stub handler that does not capture the request.
        /// </summary>
        private IApiHelper CreateApiHelper(HttpResponseMessage responseMessage, out HttpClient httpClient)
        {
            var stubHandler = new HttpMessageHandlerStub(responseMessage);
            httpClient = new HttpClient(stubHandler);
            var logger = new LoggerFactory().CreateLogger<ApiHelper>();
            return new ApiHelper(httpClient, logger);
        }

        /// <summary>
        /// Creates an ApiHelper using a capturing handler that records the outgoing request.
        /// </summary>
        private IApiHelper CreateCapturingApiHelper(HttpResponseMessage responseMessage, out CapturingHttpMessageHandler capturingHandler, out HttpClient httpClient)
        {
            capturingHandler = new CapturingHttpMessageHandler(responseMessage);
            httpClient = new HttpClient(capturingHandler);
            var logger = new LoggerFactory().CreateLogger<ApiHelper>();
            return new ApiHelper(httpClient, logger);
        }

        [Fact]
        public async Task SendRequestAsync_ReturnsExpectedResponse_ForJsonPayload()
        {
            // Arrange
            var expectedResponseContent = "{\"result\":\"success\"}";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedResponseContent)
            };

            var apiHelper = CreateApiHelper(responseMessage, out var httpClient);
            var jsonPayload = "{\"message\":\"Hello\"}";
            var apiUrl = "https://example.com/api";

            // Act
            var result = await apiHelper.SendRequestAsync(jsonPayload, apiUrl);

            // Extract the response content as string
            var resultContent = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedResponseContent, resultContent);
        }

        [Fact]
        public async Task SendFormUrlEncodedRequestAsync_ReturnsExpectedResponse_ForFormData()
        {
            // Arrange
            var expectedResponseContent = "{\"result\":\"form success\"}";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedResponseContent)
            };

            var apiHelper = CreateApiHelper(responseMessage, out var httpClient);
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("key", "value")
            };
            var apiUrl = "https://example.com/api/form";

            // Act
            var result = await apiHelper.SendFormUrlEncodedRequestAsync(formData, apiUrl,null);

            // Extract the response content as string
            var resultContent = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedResponseContent, resultContent);
        }

        [Fact]
        public async Task SendRequestAsync_ThrowsException_OnHttpError()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            var apiHelper = CreateApiHelper(responseMessage, out var httpClient);
            var jsonPayload = "{\"message\":\"Error\"}";
            var apiUrl = "https://example.com/api";

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => apiHelper.SendRequestAsync(jsonPayload, apiUrl));
        }

        [Fact]
        public async Task SendRequestAsync_AddsIdempotencyAndAuthHeaders_WhenProvided()
        {
            // Arrange
            var expectedResponseContent = "{\"result\":\"headers success\"}";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedResponseContent)
            };

            var apiHelper = CreateCapturingApiHelper(responseMessage, out var capturingHandler, out var httpClient);
            var jsonPayload = "{\"message\":\"Hello\"}";
            var apiUrl = "https://example.com/api";
            var idempotencyKey = "unique-key";
            var authToken = "test-token";

            var headers = new Dictionary<string, string>
                {
                    { "Idempotency-Key", idempotencyKey },
                    { "Authorization", authToken }
                };

            // Act
            var result = await apiHelper.SendRequestAsync(jsonPayload, apiUrl,headers);

            // Extract the response content as string
            var resultContent = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedResponseContent, resultContent);
            Assert.NotNull(capturingHandler.CapturedRequest);
            Assert.True(capturingHandler.CapturedRequest.Headers.Contains("Idempotency-Key"));
            Assert.True(capturingHandler.CapturedRequest.Headers.Contains("Authorization"));
        }

        [Fact]
        public async Task SendFormUrlEncodedRequestAsync_AddsIdempotencyAndAuthHeaders_WhenProvided()
        {
            // Arrange
            var expectedResponseContent = "{\"result\":\"form headers success\"}";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(expectedResponseContent)
            };

            var apiHelper = CreateCapturingApiHelper(responseMessage, out var capturingHandler, out var httpClient);
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", "testuser"),
                new KeyValuePair<string, string>("password", "secret")
            };
            var apiUrl = "https://example.com/api/form";
            var idempotencyKey = "form-unique-key";
            var authToken = "form-test-token";

            var headers = new Dictionary<string, string>
                {
                    { "Idempotency-Key", idempotencyKey },
                    { "Authorization", authToken }
                };

            // Act
            var result = await apiHelper.SendFormUrlEncodedRequestAsync(formData, apiUrl, headers);

            // Extract the response content as string
            var resultContent = await result.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedResponseContent, resultContent);
            Assert.NotNull(capturingHandler.CapturedRequest);
            Assert.True(capturingHandler.CapturedRequest.Headers.Contains("Idempotency-Key"));
            Assert.True(capturingHandler.CapturedRequest.Headers.Contains("Authorization"));
        }
    }
}