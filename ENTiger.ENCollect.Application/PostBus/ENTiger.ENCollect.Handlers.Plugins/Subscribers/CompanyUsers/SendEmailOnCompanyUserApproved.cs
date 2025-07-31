using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Net;
using System.Web;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public partial class SendEmailOnCompanyUserApproved : ISendEmailOnCompanyUserApproved
    {
        protected readonly ILogger<SendEmailOnCompanyUserApproved> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly ICustomUtility _customUtility;
        private ApplicationUser? appUser;
        private string subject { get; set; }
        private string authUrl = string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly IApiHelper _apiHelper;

        public SendEmailOnCompanyUserApproved(ILogger<SendEmailOnCompanyUserApproved> logger, IRepoFactory repoFactory
            ,ISmsUtility smsUtility, IEmailUtility emailUtility, MessageTemplateFactory messageTemplateFactory
            , IOptions<AuthSettings> authSettings, ICustomUtility customUtility, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _authSettings = authSettings.Value;
            _customUtility = customUtility;
            _apiHelper = apiHelper;
        }

        public virtual async Task Execute(CompanyUserApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            _logger.LogInformation("SendEmailOnCompanyUserApproved : Start");
            authUrl = _authSettings.AuthUrl;
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);
            string tenantId = @event.AppContext.TenantId;

            string authapiUrl = authUrl;

            foreach (var companyuserid in @event.Ids)
            {
                _logger.LogInformation("SendEmailOnCompanyUserApproved : UserId - " + companyuserid);
                var companyuserdto = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                                .ByCompanyUserId(companyuserid)
                                                .SelectTo<CompanyUserDtoWithId>().FirstOrDefaultAsync();
                companyuserdto.SetAppContext(@event.AppContext);

                //TODO: Write your business logic here:
                string baseUrl = _authSettings.BaseUrl;
                var data = new
                {
                    email = tenantId + "_" + companyuserdto.PrimaryEMail,
                    password = _authSettings.Password,
                    confirmPassword = _authSettings.ConfirmPassword,
                    firstName = "test",
                    middleName = "",
                    lastName = ""
                };
                string jsonPayload = JsonConvert.SerializeObject(data);
                var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
                string authUserId = await checkExistName.Content.ReadAsStringAsync();
                _logger.LogInformation("Log authUserId " + authUserId);
                if (string.IsNullOrEmpty(authUserId))
                {
                    _logger.LogWarning("ApprovedStaffEmailService : Invalid Auth User");
                }
                else
                {
                    appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).FirstOrDefaultAsync();
                }
                if (appUser != null)
                {
                    var forgotPasswordresponse = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/ForgotPassword", HttpMethod.Post);
                    _logger.LogInformation("ApprovedStaffEmailService :  StatusCode - " + forgotPasswordresponse.IsSuccessStatusCode);
                    if (!forgotPasswordresponse.IsSuccessStatusCode)
                    {
                        _logger.LogWarning("ApprovedStaffEmailService : Error in Auth while generating code");
                    }
                    else
                    {
                        try
                        {
                            var code = forgotPasswordresponse.Content.ReadAsStringAsync().Result;
                            var shortcode = _customUtility.GenerateRandomCode();

                            UserVerificationCodes usercodes = new UserVerificationCodes();
                            usercodes.ShortVerificationCode = shortcode;
                            usercodes.VerificationCode = code;
                            usercodes.UserVerificationCodeTypeId = UserVerificationCodeTypeEnum.ForgotPassword.Value;
                            usercodes.UserId = appUser.Id;
                            usercodes.SetAddedOrModified();
                            _repoFactory.GetRepo().InsertOrUpdate(usercodes);
                            await _repoFactory.GetRepo().SaveAsync();

                            if (code != null)
                            {
                                subject = "ApprovedStaff";
                                var callbackUrl = baseUrl + "#/ResetPassword?Code=" + shortcode + "&Email=" + companyuserdto.PrimaryEMail + "&TenantId=" + tenantId;
                                string tinyUrl = string.Empty;
                                try
                                {
                                    string encodedUrl = HttpUtility.UrlEncode(callbackUrl);
                                    string apiUrl = $"http://tinyurl.com/api-create.php?url={encodedUrl}";
                                    var response = await _apiHelper.SendRequestAsync("", apiUrl, HttpMethod.Get);
                                    tinyUrl = await response.Content.ReadAsStringAsync();
                                    System.Diagnostics.Trace.WriteLine(tinyUrl);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError("Exception in ApprovedStaffEmailService : Generate tinyUrl - " + ex);
                                }

                                _logger.LogInformation("ApprovedStaffEmailService : Send Email - " + companyuserdto.PrimaryEMail);

                                var messageTemplate = _messageTemplateFactory.CompanyUserApprovedEmailTemplate(companyuserdto, callbackUrl);

                                await _emailUtility.SendEmailAsync(companyuserdto.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

                                _logger.LogInformation("ApprovedStaffEmailService : EMail Sent Successfully.");

                                _logger.LogInformation("ApprovedStaffSMSService : Send SMS - " + companyuserdto.PrimaryEMail);
                                var smsmessageTemplate = _messageTemplateFactory.CompanyUserApprovedSMSTemplate(companyuserdto, tinyUrl);
                                await _smsUtility.SendSMS(companyuserdto.PrimaryMobileNumber, smsmessageTemplate.SMSMessage, @event.AppContext.TenantId);
                                _logger.LogInformation("ApprovedStaffSMSService : SMS Sent Successfully.");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError("Exception in UserVerificationCode Generation " + ex);
                        }
                    }
                }
            }
            //EventCondition = CONDITION_ONSUCCESS;
            // await this.Fire<SendEmailOnCompanyUserApproved>(EventCondition, serviceBusContext);
            await Task.CompletedTask;
        }
    }
}