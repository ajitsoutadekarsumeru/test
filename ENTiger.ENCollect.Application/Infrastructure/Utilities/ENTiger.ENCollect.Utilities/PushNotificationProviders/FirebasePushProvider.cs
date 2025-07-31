using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ENTiger.ENCollect;

public class FirebasePushProvider : IPushNotificationProvider
{
    protected readonly ILogger<FirebasePushProvider> _logger;
    private readonly GoogleSettings _googleSettings;
    private readonly GoogleCredential _googleCredential;
    private readonly IApiHelper _apiHelper;
    public FirebasePushProvider(ILogger<FirebasePushProvider> logger, IOptions<GoogleSettings> googleSettings, IApiHelper apiHelper)
    {
        _logger = logger;
        _googleSettings = googleSettings.Value;
        _googleCredential = GoogleCredential
                .FromFile(_googleSettings.FireBase.CredentialJsonPath)//("serviceAccountJsonPath")
                .CreateScoped(_googleSettings.FireBase.ScopeUrl);
        _apiHelper = apiHelper;
    }
    public async Task<bool> SendNotificationAsync(NotificationRequest notification)
    {
        bool result = false;
        var accessToken = await _googleCredential.UnderlyingCredential.GetAccessTokenForRequestAsync();

        var message = new
        {
            message = new
            {
                token = notification.Recipient,
                notification = new
                {
                    title = notification.Title,
                    body = notification.Message
                }
            }
        };

        var jsonMessage = JsonConvert.SerializeObject(message);
        var headers = new Dictionary<string, string>
        {
            { "Bearer", accessToken }
        };

        var response = await _apiHelper.SendRequestAsync(jsonMessage, _googleSettings.FireBase.Url, HttpMethod.Post, headers);

        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            result = true;
            _logger.LogInformation("Notification sent successfully via {Provider}. StatusCode: {StatusCode}, Response: {ResponseContent}",
                "FireBase", response.StatusCode, responseContent);
        }
        else
        {
            result = false;
            _logger.LogError("Failed to send notification via {Provider}. StatusCode: {StatusCode}, Response: {ResponseContent}",
                "FireBase", response.StatusCode, responseContent);
        }
        return result;
    }
}
