using ENCollect.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Text;

namespace ENTiger.ENCollect.ApplicationUsersModule.ADLoginApplicationUsersPlugins
{
    public partial class ValidateLoginUser : FlexiBusinessRuleBase, IFlexiBusinessRule<ADLoginDataPacket>
    {
        public override string Id { get; set; } = "ValidateUser";
        public override string FriendlyName { get; set; } = "ValidateUser";
        protected readonly ILogger<ValidateLoginUser> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IUserUtility _userUtility;
        protected FlexAppContextBridge? _flexAppContext;

        public ValidateLoginUser(ILogger<ValidateLoginUser> logger, IRepoFactory repoFactory, IUserUtility userUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            this._userUtility = userUtility;
        }

        public virtual async Task Validate(ADLoginDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();  //do not remove this line
            if (!packet.HasError)
            {
                string key = packet.Key;
                string userName;
                ApplicationUser appUser;
                var aesGcmCrypto = new AesGcmCrypto();
                var aesGcmKey = Encoding.UTF8.GetBytes(key);
                userName = aesGcmCrypto.Decrypt(packet.Dto.UserName, aesGcmKey);

                appUser = await _repoFactory.GetRepo().FindAll<CompanyUser>().Where(x => x.DomainId == userName && x.IsDeleted == false).FirstOrDefaultAsync();

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

                    if (string.Equals(loginDetailsHistory.LoginStatus, LoginStatusEnum.Fail.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        //save loginDetailHistory
                        loginDetailsHistory.SetAddedOrModified();
                        _repoFactory.GetRepo().InsertOrUpdate(loginDetailsHistory);
                        int records = await _repoFactory.GetRepo().SaveAsync();
                        if (records > 0)
                        {
                            _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(AgencyUser).Name, loginDetailsHistory.Id);
                        }
                        else
                        {
                            _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(AgencyUser).Name, loginDetailsHistory.Id);
                        }
                    }
                }
            }

            await Task.CompletedTask;
        }
    }
}