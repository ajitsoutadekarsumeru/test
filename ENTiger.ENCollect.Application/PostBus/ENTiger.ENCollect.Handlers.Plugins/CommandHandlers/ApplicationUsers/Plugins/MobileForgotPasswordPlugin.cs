using Elastic.Clients.Elasticsearch.MachineLearning;
using Elastic.Clients.Elasticsearch.Nodes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Reflection.PortableExecutable;
using System;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class MobileForgotPasswordPlugin : FlexiPluginBase, IFlexiPlugin<MobileForgotPasswordPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1347a7187da348569df731cac0b87d";
        public override string FriendlyName { get; set; } = "MobileForgotPasswordPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<MobileForgotPasswordPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        private readonly ICustomUtility _customUtility;
        protected ApplicationUser? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly ISmsUtility _smsUtility;
        protected readonly IEmailUtility _emailUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private string authUrl = string.Empty;

        private readonly AuthSettings _authSettings;
        private readonly IApiHelper _apiHelper;

        public MobileForgotPasswordPlugin(ILogger<MobileForgotPasswordPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory
            , ISmsUtility smsUtility, IEmailUtility emailUtility, ICustomUtility customUtility, MessageTemplateFactory messageTemplateFactory, IOptions<AuthSettings> authSettings
            , IApiHelper apiHelper)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _customUtility = customUtility;
            _authSettings = authSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Execute(MobileForgotPasswordPostBusDataPacket packet)
        {
            _logger.LogWarning("ForgotPasswordMobileMobile : Start");
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

            string jsonPayload = JsonConvert.SerializeObject(data);
            var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
            string authUserId = await checkExistName.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(authUserId))
            {
                _logger.LogWarning("ForgotPasswordMobileMobile : Auth User does not exist");
            }
            else
            {
                ApplicationUser appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).FirstOrDefaultAsync();

                if (appUser != null)
                {
                    var forgotPasswordresponse = await _apiHelper.SendRequestAsync(jsonPayload, authUrl + "/api/AccountAPI/ForgotPassword", HttpMethod.Post);
                    if (!forgotPasswordresponse.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("ForgotPasswordMobileMobile : Something went wrong. Please try again after sometime");
                    }
                    else
                    {
                        try
                        {
                            var code = forgotPasswordresponse.Content.ReadAsStringAsync().Result;
                            var shortcode = _customUtility.GenerateRandomCode();

                            UserVerificationCodes usercode = new UserVerificationCodes();
                            usercode.ForgotPasswordVerificationCode(shortcode, code, appUser.Id);

                            _repoFactory.GetRepo().InsertOrUpdate(usercode);
                            int records = await _repoFactory.GetRepo().SaveAsync();

                            if (code != null)
                            {
                                var dto = new SendMobileForgotPasswordDto
                                {
                                    Code = shortcode,
                                    FirstName = appUser.FirstName,
                                    LastName = appUser.LastName
                                };
                                dto.SetAppContext(packet.Cmd.Dto.GetAppContext());
                                var messageTemplate = _messageTemplateFactory.MobileForgotPasswordTemplate(dto);

                                if (string.IsNullOrEmpty(appUser.PrimaryMobileNumber))
                                {
                                    _logger.LogWarning("ForgotPasswordMobileMobile : User Mobile Number Not Available");
                                }
                                else
                                {
                                    _logger.LogInformation("ForgotPasswordMobileMobile : Send SMS - " + appUser.PrimaryMobileNumber);
                                    await _smsUtility.SendSMS(appUser.PrimaryMobileNumber, messageTemplate.SMSMessage, tenantId);
                                    _logger.LogInformation("ForgotPasswordMobileMobile : SMS Sent Successfully.");
                                }
                                if (string.IsNullOrEmpty(appUser.PrimaryEMail))
                                {
                                    _logger.LogWarning("ForgotPasswordMobileMobile : User EMail Not Available");
                                }
                                else
                                {
                                    _logger.LogInformation("ForgotPasswordMobileMobile : Send Email - " + appUser.PrimaryEMail);
                                    await _emailUtility.SendEmailAsync(appUser.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, tenantId);
                                    _logger.LogInformation("ForgotPasswordMobileMobile : EMail Sent Successfully.");
                                }

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
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Exception in ForgotPasswordMobileMobile : GetCode and Send - " + ex);
                        }
                    }
                }
                else
                {
                    _logger.LogWarning("ForgotPasswordMobileMobile : Application User does not exist");
                }
            }

            _logger.LogInformation("ForgotPasswordMobileMobile : End");
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}