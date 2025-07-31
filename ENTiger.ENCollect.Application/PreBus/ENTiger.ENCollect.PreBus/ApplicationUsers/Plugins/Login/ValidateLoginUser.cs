using ENCollect.Security;
using ENTiger.ENCollect.ApplicationUsersModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.PreBus.ApplicationUsers.Plugins.Login
{
    public partial class ValidateLoginUser : FlexiBusinessRuleBase, IFlexiBusinessRule<LoginDataPacket>
    {
        public override string Id { get; set; } = "ValidateUser";
        public override string FriendlyName { get; set; } = "ValidateUser";
        protected readonly ILogger<ValidateLoginUser> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IUserUtility _userUtility;

        public ValidateLoginUser(ILogger<ValidateLoginUser> logger, IRepoFactory repoFactory, IUserUtility userUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _userUtility = userUtility;
        }

        public virtual async Task Validate(LoginDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext(); //do not remove this line
            if (!packet.HasError)
            {
                string key = packet.Key;
                string userName;
                ApplicationUser? appUser;

                var aesGcmCrypto = new AesGcmCrypto();
                var aesGcmKey = Encoding.UTF8.GetBytes(key);

                userName = aesGcmCrypto.Decrypt(packet.Dto.UserName, aesGcmKey);

                if (!userName.Contains("."))
                {
                    appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                        .Where(x => x.PrimaryMobileNumber == userName && x.IsDeleted == false)
                                        .FirstOrDefaultAsync();
                }
                else
                {
                    appUser = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                        .Where(x => x.PrimaryEMail == userName && x.IsDeleted == false)
                                        .FirstOrDefaultAsync();
                }

                if (appUser != null)
                {
                    LoginDetailsHistory loginDetailsHistory = new LoginDetailsHistory();
                    loginDetailsHistory.UserId = appUser.Id;

                    UserUtility userUtility;
                    string errormessage = await _userUtility.ValidateUserStatus(appUser, packet.Dto);

                    if (!string.IsNullOrEmpty(errormessage))
                    {
                        loginDetailsHistory.LoginStatus = LoginStatusEnum.Fail.Value;
                        loginDetailsHistory.Remarks = errormessage;
                        packet.AddError("Error", errormessage);
                    }                    

                    if (loginDetailsHistory.LoginStatus == LoginStatusEnum.Fail.Value)
                    {
                        //save loginDetailHistory
                        loginDetailsHistory.SetAddedOrModified();
                        _repoFactory.GetRepo().InsertOrUpdate(loginDetailsHistory);
                        int records = await _repoFactory.GetRepo().SaveAsync();
                        if (records > 0)
                        {
                            _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ",
                                typeof(AgencyUser).Name, loginDetailsHistory.Id);
                        }
                        else
                        {
                            _logger.LogWarning("No records inserted for {Entity} with {EntityId}",
                                typeof(AgencyUser).Name, loginDetailsHistory.Id);
                        }
                    }
                }
            }

            await Task.CompletedTask;
        }

    }
}