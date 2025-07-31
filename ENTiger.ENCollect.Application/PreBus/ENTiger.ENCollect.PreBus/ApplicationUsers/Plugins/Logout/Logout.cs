using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule.LogoutApplicationUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Logout : FlexiBusinessRuleBase, IFlexiBusinessRule<LogoutDataPacket>
    {
        public override string Id { get; set; } = "3a1337c86f912005113ba5b33f5b10d7";
        public override string FriendlyName { get; set; } = "Logout";

        protected readonly ILogger<Logout> _logger;
        protected readonly IRepoFactory _repoFactory;
        string authUrl =  string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly IApiHelper _apiHelper;

        public Logout(ILogger<Logout> logger, IRepoFactory repoFactory, IOptions<AuthSettings> authSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Validate(LogoutDataPacket packet)
        {
            authUrl = _authSettings.AuthUrl;
            _repoFactory.Init(packet.Dto);

            string authURL = authUrl;

            var data = new
            {
                token_type_hint = "access_token",
                token = packet.Dto.Token,
                Client_id = "ENTigerClient",
                Client_secret = _authSettings.Password
            };

            string jsonPayload = JsonConvert.SerializeObject(data);
            var response = await _apiHelper.SendRequestAsync(jsonPayload, authURL + "/api/AccountAPI/logout", HttpMethod.Post);
            if (!response.IsSuccessStatusCode)
            {
                packet.AddError("Error", response.Content.ReadAsStringAsync().Result);
            }
            var result = response.Content.ReadAsStringAsync().Result;
            if (result != null)
            {
                //packet.OutputModel = JsonConvert.DeserializeObject<TokenOutputAPIModel>(result);
            }
        }
    }
}
