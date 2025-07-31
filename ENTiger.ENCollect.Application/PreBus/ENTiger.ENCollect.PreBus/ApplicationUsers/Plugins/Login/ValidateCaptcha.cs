using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule.LoginApplicationUsersPlugins
{
    public partial class ValidateCaptcha : FlexiBusinessRuleBase, IFlexiBusinessRule<LoginDataPacket>
    {
        public override string Id { get; set; } = "3a12e5ddd8fa5eb21c4371769a327aaf";
        public override string FriendlyName { get; set; } = "ValidateCaptcha";

        private readonly ICustomUtility _customUtility;
        protected readonly ILogger<ValidateCaptcha> _logger;
        protected readonly IRepoFactory _repoFactory;
        private readonly GoogleSettings _googleSettings;

        public ValidateCaptcha(ILogger<ValidateCaptcha> logger, IRepoFactory repoFactory, IOptions<GoogleSettings> googleSettings, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
            _googleSettings = googleSettings.Value;
        }

        /// <summary>
        /// Validates the captcha token provided in the login data packet.
        /// </summary>
        /// <param name="packet">The login data packet containing the captcha token.</param>
        public virtual async Task Validate(LoginDataPacket packet)
        {
            bool isCaptchaEnabled = _googleSettings.Captcha.Enabled;
            _repoFactory.Init(packet.Dto);

            _logger.LogInformation("ValidateCaptcha : Start");
            try
            {
                _logger.LogInformation("ValidateCaptcha : isCaptchaEnabled - {isCaptchaEnabled}", isCaptchaEnabled);
                if (isCaptchaEnabled)
                {
                    string url = _googleSettings.Captcha.Url;
                    string gkey = _googleSettings.Captcha.SecretKey;

                    _logger.LogInformation("ValidateCaptcha : GoogleCaptchaUrl - {url}", url);
                    if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(gkey))
                    {
                        packet.AddError("Error", "Please configure the Captcha Service");
                    }
                    else
                    {
                        _logger.LogInformation("ValidateCaptcha : GoogleSecretKey - {gkey} | Token - {token}", gkey, packet.Dto.Token);
                        using HttpClient client = new HttpClient();
                        var data = new
                        {
                            secret = gkey,
                            response = packet.Dto.Token
                        };

                        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
                        {
                            Content = new StringContent(_customUtility.JsonToQuery(JsonConvert.SerializeObject(data)), Encoding.UTF8, "application/x-www-form-urlencoded")
                        };

                        HttpResponseMessage response = await client.SendAsync(request);
                        if (!response.IsSuccessStatusCode)
                        {
                            packet.AddError("Error", "Internal Error, please contact administrator");
                        }
                        else
                        {
                            string result = await response.Content.ReadAsStringAsync();
                            _logger.LogInformation("ValidateCaptcha : Response - {result}", result);
                            ResponseToken obj = JsonConvert.DeserializeObject<ResponseToken>(result);
                            if (!obj.Success)
                            {
                                packet.AddError("Error", "Invalid Captcha");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in ValidateCaptcha");
                packet.AddError("Error", "Internal Error while validating captcha, please contact administrator");
            }
            _logger.LogInformation("ValidateCaptcha : End");
        }

        /// <summary>
        /// Represents the response from the Google Captcha verification service.
        /// </summary>
        public class ResponseToken
        {
            public DateTime challenge_ts { get; set; }
            public float score { get; set; }
            public List<string> ErrorCodes { get; set; }
            public bool Success { get; set; }
            public string hostname { get; set; }
        }
    }
}
