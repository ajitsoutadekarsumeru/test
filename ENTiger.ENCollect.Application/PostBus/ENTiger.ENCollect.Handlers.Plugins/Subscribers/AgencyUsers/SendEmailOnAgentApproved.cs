using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Net;
using System.Web;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class SendEmailOnAgentApproved : ISendEmailOnAgentApproved
    {
        protected readonly ILogger<SendEmailOnAgentApproved> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private string authUrl = string.Empty;
        private string tenantId = string.Empty;
        private string shortcode = string.Empty;
        protected readonly IEmailUtility _emailUtility;
        protected readonly ISmsUtility _smsUtility;
        protected readonly MessageTemplateFactory _messageTemplateFactory;
        private readonly AuthSettings _authSettings;
        private readonly ICustomUtility _customUtility;
        private readonly IApiHelper _apiHelper;
        public SendEmailOnAgentApproved(ILogger<SendEmailOnAgentApproved> logger, ICustomUtility customUtility,IRepoFactory repoFactory
            , ISmsUtility smsUtility, IEmailUtility emailUtility,MessageTemplateFactory messageTemplateFactory
            , IOptions<AuthSettings> authSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _smsUtility = smsUtility;
            _emailUtility = emailUtility;
            _messageTemplateFactory = messageTemplateFactory;
            _customUtility = customUtility;
            _authSettings = authSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Execute(AgentApproved @event, IFlexServiceBusContext serviceBusContext)
        {
            authUrl = _authSettings.AuthUrl;
            _flexAppContext = @event.AppContext;
            _repoFactory.Init(@event);
            tenantId = _flexAppContext.TenantId;

            foreach (var agentId in @event.Ids)
            {
                var agencyuserdto = await _repoFactory.GetRepo().FindAll<AgencyUser>().ByAgencyUserId(agentId).SelectTo<AgencyUserDtoWithId>().FirstOrDefaultAsync();

                agencyuserdto.SetAppContext(@event.AppContext);
                string emailId = agencyuserdto.PrimaryEMail;
                string UserName = agencyuserdto.FirstName + " " + agencyuserdto.LastName;
                string userId = agencyuserdto.UserId;
                string code = await GetCode(emailId, tenantId);

                if (!string.IsNullOrEmpty(code))
                {
                    string baseUrl = _authSettings.BaseUrl;
                    _logger.LogDebug("ApprovedAgentEmailService : BaseURL : " + baseUrl);

                    var callbackUrl = baseUrl + "#/ResetPassword?Code=" + shortcode + "&Email=" + emailId + "&TenantId=" + tenantId;
                    string message = "Your id has been approved by Encollect team. Your profile User Name : " + emailId
                    + ". To create your password, " + "<a href=\"" + callbackUrl + "\" > CLICK HERE</a> " + "<br/><br/>";

                    _logger.LogDebug("ApprovedAgentEmailService : Send Email - " + emailId);

                    var messageTemplate = _messageTemplateFactory.AgencyUserApprovedEmailTemplate(agencyuserdto, callbackUrl);

                    _logger.LogInformation("SendEMailOnAgencyUserCreated : Email - " + agencyuserdto.PrimaryEMail);
                    await _emailUtility.SendEmailAsync(agencyuserdto.PrimaryEMail, messageTemplate.EmailMessage, messageTemplate.EmailSubject, @event.AppContext.TenantId);

                    _logger.LogDebug("ApprovedAgentEmailService : EMail Sent Successfully.");
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
                        _logger.LogError("Exception in ApprovedAgentEmailService : Generate tinyUrl - " + ex);
                    }
                    tinyUrl = string.IsNullOrEmpty(tinyUrl) ? "NotAvailable" : tinyUrl;

                    var smsmessageTemplate = _messageTemplateFactory.AgencyUserApprovedSMSTemplate(agencyuserdto, tinyUrl);

                    _logger.LogDebug("ApprovedAgentEmailService : Send SMS - " + agencyuserdto.PrimaryMobileNumber);
                    await _smsUtility.SendSMS(agencyuserdto.PrimaryMobileNumber, smsmessageTemplate.SMSMessage, @event.AppContext.TenantId);
                    _logger.LogDebug("ApprovedAgentEmailService : SMS Sent Successfully.");
                }
                else
                {
                    _logger.LogCritical("ApprovedAgentEmailService : Invalid Code");
                }
            }

            //TODO: Specify your condition to raise event here...
            //TODO: Set the value of OnRaiseEventCondition according to your business logic

            //EventCondition = CONDITION_ONSUCCESS;

            //await this.Fire<SendEmailOnAgentApproved>(EventCondition, serviceBusContext);
            await Task.CompletedTask;
        }

        public async Task<string> GetCode(string strEmail, string tenantId)
        {
            _logger.LogDebug("ApprovedAgentEmailService : GetCode - Start | Email : " + strEmail);
            try
            {
                ApplicationUser? appUser;
                string authapiUrl;

                authapiUrl = authUrl;
                _logger.LogDebug("ApprovedAgentEmailService : GetCode | AuthURL : " + authapiUrl);

                var data = new
                {
                    email = tenantId + "_" + strEmail,
                    password = _authSettings.Password,
                    confirmPassword = _authSettings.ConfirmPassword,
                    firstName = "test",
                    middleName = "",
                    lastName = ""
                };
                string jsonPayload = JsonConvert.SerializeObject(data);

                var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/CheckExistEmailName", HttpMethod.Post);
                string authUserId = await checkExistName.Content.ReadAsStringAsync();
                if (string.IsNullOrEmpty(authUserId))
                {
                    _logger.LogDebug("ApprovedAgentEmailService : Invalid Auth User");
                    return "";
                }
                else
                {
                    appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).FirstOrDefaultAsync();

                    if (appUser != null)
                    {
                        var forgotPasswordresponse = await _apiHelper.SendRequestAsync(jsonPayload, authapiUrl + "/api/AccountAPI/ForgotPassword", HttpMethod.Post);
                        _logger.LogInformation("ApprovedAgentEmailService :  StatusCode - " + forgotPasswordresponse.IsSuccessStatusCode);
                        if (!forgotPasswordresponse.IsSuccessStatusCode)
                        {
                            _logger.LogDebug("ApprovedAgentEmailService : Error in Auth while generating code");
                            return "";
                        }
                        else
                        {
                            try
                            {
                                var code = forgotPasswordresponse.Content.ReadAsStringAsync().Result;
                                shortcode = _customUtility.GenerateRandomCode();

                                UserVerificationCodes usercodes = new UserVerificationCodes();

                                usercodes.ShortVerificationCode = shortcode;
                                usercodes.VerificationCode = code;
                                usercodes.UserVerificationCodeTypeId = await _repoFactory.GetRepo().FindAll<UserVerificationCodeTypes>()
                                                                                    .Where(i => string.Equals(i.Description, "forgotpassword")).Select(i => i.Id)
                                                                                    .FirstOrDefaultAsync();
                                usercodes.UserId = appUser.Id;
                                usercodes.SetAdded();

                                _repoFactory.GetRepo().InsertOrUpdate(usercodes);
                                await _repoFactory.GetRepo().SaveAsync();
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("Exception in UserVerificationCode Generation " + ex);
                            }
                            if (shortcode != null)
                            {
                                return shortcode;
                            }
                            else
                            {
                                return "";
                            }
                        }
                    }
                    else
                    {
                        _logger.LogDebug("ApprovedAgentEmailService : Invalid Application User");
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ApprovedAgentEmailService : GetCode " + ex);
                return "";
            }
        }
    }
}