using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ENCollect.Security;
using Sumeru.Flex;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Elastic.Clients.Elasticsearch.MachineLearning;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect.ApplicationUsersModule.ChangePasswordApplicationUsersPlugins
{
    public partial class ChangePassword : FlexiBusinessRuleBase, IFlexiBusinessRule<ChangePasswordDataPacket>
    {
        public override string Id { get; set; } = "3a143e5b5f360c2f583134a10924d947";
        public override string FriendlyName { get; set; } = "ChangePassword";

        protected readonly ILogger<ChangePassword> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        string authUrl = string.Empty;
        private readonly AuthSettings _authSettings;
        private readonly IApiHelper _apiHelper;

        public ChangePassword(ILogger<ChangePassword> logger, IRepoFactory repoFactory, IOptions<AuthSettings> authSettings, IApiHelper apiHelper)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _authSettings = authSettings.Value;
            _apiHelper = apiHelper;
        }

        public virtual async Task Validate(ChangePasswordDataPacket packet)
        {
            authUrl = _authSettings.AuthUrl;
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();
            string tenantId = _flexAppContext.TenantId;

            if (!packet.HasError)
            {
                try
                {
                    _logger.LogInformation("Start  ChangePasswordFFPlugin : ");
                    _logger.LogInformation("Start  ChangePasswordFFPlugin input Json : " + JsonConvert.SerializeObject(packet));

                    string emailId = string.Empty;
                    string key = packet.Key;
                    ApplicationUser? user = new ApplicationUser();

                    string oldPassword = string.Empty;
                    string password = string.Empty;
                    string confirmPassword = string.Empty;
                    var aesGcmCrypto = new AesGcmCrypto();
                    var aesGcmKey = Encoding.UTF8.GetBytes(key);

                    oldPassword = aesGcmCrypto.Decrypt(packet.Dto.OldPassword, aesGcmKey);
                    password = aesGcmCrypto.Decrypt(packet.Dto.Password, aesGcmKey);
                    confirmPassword = aesGcmCrypto.Decrypt(packet.Dto.ConfirmPassword, aesGcmKey);
                    if(password != confirmPassword)
                    {
                        packet.AddError("Error", "The password and confirmation password do not match.");
                    }
                    else if (oldPassword==password)
                    {
                        packet.AddError("Error", "Please choose a new password that is different from the current one.");
                    }
                    _logger.LogInformation("oldPassword : " + oldPassword);
                    _logger.LogInformation("password : " + password);
                    _logger.LogInformation("confirmPassword : " + confirmPassword);
                    var decryptdata = new
                    {
                        OldPassword = oldPassword,
                        Password = password,
                        ConfirmPassword = confirmPassword
                    };
                    _logger.LogInformation("UserId : " + _flexAppContext.UserId);

                    user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == _flexAppContext.UserId).FirstOrDefaultAsync();
                    
                    if (user != null)
                    {
                        _logger.LogInformation("After fetch  User : ");
                        emailId = user.PrimaryEMail;
                        
                        _logger.LogInformation("After fetch  authapiUrl : " + authUrl);
                        
                        var data = new
                        {
                            email = tenantId + "_" + emailId,
                            password = decryptdata.OldPassword,
                            confirmPassword = decryptdata.OldPassword,
                            firstName = user.FirstName,
                            middleName = "",
                            lastName = ""
                        };
                        string jsonPayload = JsonConvert.SerializeObject(data);
                        _logger.LogInformation("ChangePasswordFFPlugin data Json  : " + jsonPayload);
                        var checkExistName = await _apiHelper.SendRequestAsync(jsonPayload, authUrl + "/api/AccountAPI/CheckExistEmailName",HttpMethod.Post);
                        string authUserId = await checkExistName.Content.ReadAsStringAsync();
                        _logger.LogInformation("After fetch  authUserId : " + authUserId);
                        if (string.IsNullOrEmpty(authUserId))
                        {
                            _logger.LogInformation("Error, The user either does not exist or is not confirmed.");
                            packet.AddError("Error", "The user either does not exist or is not confirmed.");
                        }
                        else
                        {
                            var changepwddata = new
                            {
                                email = tenantId + "_" + emailId,
                                password = decryptdata.OldPassword,
                                newPassword = decryptdata.Password,
                                firstName = user.FirstName,
                                middleName = "",
                                lastName = ""
                            };
                            string changepwdJsonPayload = JsonConvert.SerializeObject(changepwddata);
                            var changePasswordresponse = await _apiHelper.SendRequestAsync(changepwdJsonPayload, authUrl + "/api/AccountAPI/ChangePassword", HttpMethod.Post);
                            var res = await changePasswordresponse.Content.ReadAsStringAsync();
                            if (!changePasswordresponse.IsSuccessStatusCode)
                            {
                                _logger.LogInformation("Error : " + res);
                                _logger.LogInformation("Error,Invalid Request. Please contact administrator.");
                                packet.AddError("Error", "Invalid Request. Please contact administrator.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Exception in ChangePasswordFFPlugin " + ex);
             
                    packet.AddError("Error", "Please contact administrator.");
                }
            }

            await Task.CompletedTask; //If you have any await in the validation, remove this line
        }
    }
}
