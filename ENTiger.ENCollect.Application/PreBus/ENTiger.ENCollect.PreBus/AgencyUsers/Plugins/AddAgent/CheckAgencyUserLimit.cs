using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule.AddAgentAgencyUsersPlugins
{
    public partial class CheckAgencyUserLimit : FlexiBusinessRuleBase, IFlexiBusinessRule<AddAgentDataPacket>
    {
        public override string Id { get; set; } = "f913bb50e88a4b9fa8bd4e4eb6c0c682";
        public override string FriendlyName { get; set; } = "CheckAgencyUserLimit";

        protected readonly ILogger<CheckAgencyUserLimit> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        ILicenseService _licenseService;

        public CheckAgencyUserLimit(ILogger<CheckAgencyUserLimit> logger, IRepoFactory repoFactory, ILicenseService licenseService)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _licenseService = licenseService;
        }

        public virtual async Task Validate(AddAgentDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);

            UserTypeEnum userType = Enum.TryParse(packet.Dto.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
            var validateResult = await _licenseService.ValidateUserLicenseLimitAsync(userType, packet.Dto);
            if (!validateResult.Valid)
            {
                await _licenseService.LogLicenseViolationAsync(LicenseFeatureEnum.AgencyUserLimitOnCreate, validateResult.PermittedCount, validateResult.ActualCount, _flexAppContext);
                packet.AddError("Error", $"User create failed. The maximum allowed number of users ({validateResult.PermittedCount}) for the selected user type {userType.ToString()} has been reached. Please contact your administrator to increase the limit.");
            }
        }
    }
}
