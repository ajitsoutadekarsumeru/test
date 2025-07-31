using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sumeru.Flex;
using System.Reflection.PortableExecutable;
using System;
using Elastic.Clients.Elasticsearch.MachineLearning;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ForgotPasswordPlugin : FlexiPluginBase, IFlexiPlugin<ForgotPasswordPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a133db3522fe8c5072b314c8b8e5e0a";
        public override string FriendlyName { get; set; } = "ForgotPasswordPlugin";

        protected string EventCondition = "";
        private string authUrl = string.Empty;
        protected readonly ILogger<ForgotPasswordPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected ApplicationUser? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ICustomUtility _customUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly AuthSettings _authSettings;
        private readonly IApiHelper _apiHelper;

        public ForgotPasswordPlugin(ILogger<ForgotPasswordPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory
            , IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory,
            IOptions<AuthSettings> authSettings, ICustomUtility customUtility, IApiHelper apiHelper)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _customUtility = customUtility;
            _authSettings = authSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Execute(ForgotPasswordPostBusDataPacket packet)
        {
            _logger.LogInformation("ForgotPasswordPlugin : Start");
            authUrl = _authSettings.AuthUrl;
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            string tenantId = _flexAppContext.TenantId;

            string Email = packet.Cmd.Dto.Email;

            var data = new
            {
                email = tenantId + "_" + Email,
                password = _authSettings.Password,
                confirmPassword = _authSettings.ConfirmPassword,
                firstName = "test",
                middleName = "",
                lastName = ""
            };
            _logger.LogInformation("ForgotPasswordPlugin : email - " + data.email);

            string jsonPayload = JsonConvert.SerializeObject(data);
            var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
            
            string authUserId = await checkExistName.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(authUserId))
            {
                _logger.LogInformation("ForgotPasswordPlugin : Auth User does not exist");
            }
            else
            {
                ApplicationUser appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).FirstOrDefaultAsync();

                if (appUser != null)
                {
                    var forgotPasswordresponse = await _apiHelper.SendRequestAsync(jsonPayload, authUrl + "/api/AccountAPI/ForgotPassword", HttpMethod.Post);
                    if (!forgotPasswordresponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("ForgotPasswordPlugin : Something went wrong. Please try again after sometime");
                    }
                    else
                    {
                        var code = forgotPasswordresponse.Content.ReadAsStringAsync().Result;
                        var shortcode = _customUtility.GenerateRandomCode();

                        UserVerificationCodes usercode = new UserVerificationCodes();
                        usercode.ForgotPasswordVerificationCode(shortcode, code, appUser.Id);

                        _repoFactory.GetRepo().InsertOrUpdate(usercode);
                        int records = await _repoFactory.GetRepo().SaveAsync();

                        if (code != null)
                        {
                            var dto = new SendForgotPasswordDto
                            {
                                Code = shortcode,
                                Email = Email
                            };
                            dto.SetAppContext(packet.Cmd.Dto.GetAppContext());
                            var messageTemplate = _messageTemplateFactory.ForgotPasswordTemplate(dto);

                            _logger.LogInformation("ForgotPasswordPlugin : SendMail : Email - " + appUser.PrimaryEMail);
                            await _emailUtility.SendEmailAsync(appUser.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);

                            if (appUser.ForgotPasswordDateTime == null || DateTime.Now.Subtract(appUser.ForgotPasswordDateTime.Value).Hours > 24)
                            {
                                appUser.ForgotPasswordCount = 1;
                                appUser.ForgotPasswordDateTime = DateTime.Now;
                            }
                            else
                            {
                                appUser.ForgotPasswordCount = appUser.ForgotPasswordCount + 1;
                            }
                            appUser.SetAddedOrModified();
                            _repoFactory.GetRepo().InsertOrUpdate(appUser);
                            await _repoFactory.GetRepo().SaveAsync();
                        }
                        else
                        {
                            _logger.LogError("ForgotPasswordPlugin : AuthError - Error Occured while fetching code from auth application");
                        }
                    }
                }
                else
                {
                    _logger.LogInformation("ForgotPasswordPlugin : User does not exist");
                }
            }
            _logger.LogInformation("ForgotPasswordPlugin : End");
            await Task.CompletedTask;
        }
    }
}