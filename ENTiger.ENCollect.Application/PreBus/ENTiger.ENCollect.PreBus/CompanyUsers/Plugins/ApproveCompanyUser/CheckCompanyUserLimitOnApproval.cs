using ENTiger.ENCollect.DomainModels.Enum;
using ENTiger.ENCollect.Messages.Events.License;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule.ApproveCompanyUserCompanyUsersPlugins
{
    public partial class CheckCompanyUserLimitOnApproval : FlexiBusinessRuleBase, IFlexiBusinessRule<ApproveCompanyUserDataPacket>
    {
        public override string Id { get; set; } = "5bdd3d1eab0e4f058a38111959e2b3da";
        public override string FriendlyName { get; set; } = "CheckCompanyUserLimitOnApproval";


        protected readonly ILogger<CheckCompanyUserLimitOnApproval> _logger;
        protected readonly IRepoFactory _repoFactory;
       
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ILicenseService _licenseService;
        protected readonly IFlexServiceBusBridge _bus;

        public CheckCompanyUserLimitOnApproval(ILogger<CheckCompanyUserLimitOnApproval> logger, IRepoFactory repoFactory, ILicenseService licenseService, IFlexServiceBusBridge bus)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _licenseService = licenseService;
            _bus = bus;
        }

        public virtual async Task Validate(ApproveCompanyUserDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);

            List<ApplicationUser> users;
            List<string> ids = packet.Dto.companyUserIds;

            users = await _repoFactory.GetRepo().FindAll<ApplicationUser>().ByTFlexIds(ids).ToListAsync();

            foreach (var user in users)
            {
                UserTypeEnum userType = Enum.TryParse(user.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
                var validateResult = await _licenseService.ValidateUserLicenseLimitAsync(userType,packet.Dto);
                if (!validateResult.Valid)
                {
                    await _licenseService.LogLicenseViolationAsync(LicenseFeatureEnum.CompanyUserLimitOnApproval, validateResult.PermittedCount, validateResult.ActualCount, _flexAppContext);
                    packet.AddError("Error", $"User approval failed. The maximum allowed number of users ({validateResult.PermittedCount}) for the selected user type {userType.ToString()} has been reached. Please contact your administrator to increase the limit.");
                }
            }
        }
    }
}
