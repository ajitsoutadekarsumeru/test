using Elastic.Clients.Elasticsearch.Security;
using ENTiger.ENCollect.DomainModels.Enum;
using ENTiger.ENCollect.Messages.Events.License;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule.ApproveAgentAgencyUsersPlugins
{
    public partial class CheckAgencyUserLimitOnApproval : FlexiBusinessRuleBase, IFlexiBusinessRule<ApproveAgentDataPacket>
    {
        public override string Id { get; set; } = "0fabe65991ad40ee9abb9ce7902eaf55";
        public override string FriendlyName { get; set; } = "CheckAgencyUserLimitOnApproval";

        protected readonly ILogger<CheckAgencyUserLimitOnApproval> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ILicenseService _licenseService;
        protected readonly IFlexServiceBusBridge _bus;

        public CheckAgencyUserLimitOnApproval(ILogger<CheckAgencyUserLimitOnApproval> logger, IRepoFactory repoFactory, ILicenseService licenseService, IFlexServiceBusBridge bus)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _licenseService = licenseService;
            _bus = bus;
        }

        public virtual async Task Validate(ApproveAgentDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);

            List<AgencyUser> users;
            List<string> ids = packet.Dto.AgentIds;
            users = await _repoFactory.GetRepo().FindAll<AgencyUser>().ByAgencyUserIds(ids).ToListAsync();

            foreach (var user in users)
            {
                UserTypeEnum userType = Enum.TryParse(user.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
                var validateResult = await _licenseService.ValidateUserLicenseLimitAsync(userType, packet.Dto);
                if (!validateResult.Valid)
                {
                    await _licenseService.LogLicenseViolationAsync(LicenseFeatureEnum.AgencyUserLimitOnApproval, validateResult.PermittedCount, validateResult.ActualCount, _flexAppContext);
                    packet.AddError("Error", $"User approval failed. The maximum allowed number of users ({validateResult.PermittedCount}) for the selected user type {userType.ToString()} has been reached. Please contact your administrator to increase the limit.");
                }
            }
        }
    }
}
