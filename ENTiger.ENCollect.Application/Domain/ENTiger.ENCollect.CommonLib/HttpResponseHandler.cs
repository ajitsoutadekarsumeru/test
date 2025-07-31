using System.Net;

namespace ENTiger.ENCollect
{
    public static class HttpResponseHandler
    {
        public static async Task<HttpResponseMessage> HandleHttpResponse(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                // Request was successful (status code 200).
                return response;
            }
            else if (response.StatusCode >= HttpStatusCode.BadRequest &&
                     response.StatusCode < HttpStatusCode.InternalServerError)
            {
                // Handle any status code in the 400 range (Bad Request).
                string errorContent = await response.Content.ReadAsStringAsync();
                // Parse and handle the error content.
                throw new BadRequestException(errorContent);
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                // Handle 500 (Internal Server Error) error.
                string errorContent = await response.Content.ReadAsStringAsync();
                // Parse and handle the error content.
                throw new InternalServerErrorException(errorContent);
            }
            else
            {
                // Handle other HTTP error codes as needed.
                throw new Exception($"Received an unexpected status code: {response.StatusCode}");
            }
        }
    }
}