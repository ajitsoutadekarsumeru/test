using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ENTiger.ENCollect
{
    public class FcmNotificationService
    {
        private readonly string _projectId;
        private readonly GoogleCredential _googleCredential;

        public FcmNotificationService(string projectId, string serviceAccountJsonPath)
        {
            _projectId = projectId;

            _googleCredential = GoogleCredential
                .FromFile(serviceAccountJsonPath)
                .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");
        }

        public async Task SendPushAsync(string fcmToken, string title, string body)
        {
            var accessToken = await _googleCredential.UnderlyingCredential.GetAccessTokenForRequestAsync();

            var message = new
            {
                message = new
                {
                    token = fcmToken,
                    notification = new
                    {
                        title,
                        body
                    }
                }
            };

            var jsonMessage = JsonConvert.SerializeObject(message);
            var request = new HttpRequestMessage(HttpMethod.Post,
                $"https://fcm.googleapis.com/v1/projects/{_projectId}/messages:send");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

            using var httpClient = new HttpClient();
            var response = await httpClient.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine($"FCM Response: {responseContent}");
            }
            else
            {
                Console.WriteLine($"FCM Error ({response.StatusCode}): {responseContent}");
            }
        }
    }
}
