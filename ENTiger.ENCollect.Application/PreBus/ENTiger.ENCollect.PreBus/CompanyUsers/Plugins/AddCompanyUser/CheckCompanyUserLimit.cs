using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule.AddCompanyUserCompanyUsersPlugins
{
    public partial class CheckCompanyUserLimit : FlexiBusinessRuleBase, IFlexiBusinessRule<AddCompanyUserDataPacket>
    {
        public override string Id { get; set; } = "e65d5cd54c5d415e985114d4ecf98a11";
        public override string FriendlyName { get; set; } = "CheckCompanyUserLimit";

        protected readonly ILogger<CheckCompanyUserLimit> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ILicenseService _licenseService;

        public CheckCompanyUserLimit(ILogger<CheckCompanyUserLimit> logger, IRepoFactory repoFactory, ILicenseService licenseService)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _licenseService = licenseService;
        }

        public virtual async Task Validate(AddCompanyUserDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);

            UserTypeEnum userType = Enum.TryParse(packet.Dto.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
            var validateResult = await _licenseService.ValidateUserLicenseLimitAsync(userType, packet.Dto);
            if (!validateResult.Valid)
            {
                await _licenseService.LogLicenseViolationAsync(LicenseFeatureEnum.CompanyUserLimitOnCreate, validateResult.PermittedCount, validateResult.ActualCount, _flexAppContext);
                packet.AddError("Error", $"User create failed. The maximum allowed number of users ({validateResult.PermittedCount}) for the selected user type {userType.ToString()} has been reached. Please contact your administrator to increase the limit.");
            }
        }
    }
}
