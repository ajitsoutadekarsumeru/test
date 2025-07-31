using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule.AddFeedbackFeedbackPlugins
{
    public partial class CheckUserFeedbackLimit : FlexiBusinessRuleBase, IFlexiBusinessRule<AddFeedbackDataPacket>
    {
        public override string Id { get; set; } = "961872d1802447f0abdb1e7cf947e909";
        public override string FriendlyName { get; set; } = "CheckUserFeedbackLimit";

        protected readonly ILogger<CheckUserFeedbackLimit> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly IFlexServiceBusBridge _bus;
        private readonly ILicenseService _licenseService;

        public CheckUserFeedbackLimit(ILogger<CheckUserFeedbackLimit> logger, IRepoFactory repoFactory, ILicenseService licenseService, IFlexServiceBusBridge bus)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _licenseService = licenseService;
            _bus = bus;
        }

        public virtual async Task Validate(AddFeedbackDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();  //do not remove this line
            string userid = _flexAppContext.UserId;
            //get the user detail
            ApplicationUser? user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                .ByTFlexId(userid)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                UserTypeEnum userType = Enum.TryParse(user.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
                //check if user type is Others
                if (userType == UserTypeEnum.Others)
                {
                    var consumption = await _licenseService.GetFreeTrailsConsumptionAsync(userid, packet.Dto);
                    var limit = _licenseService.GetFreeTrailsLicenseLimit();
                    if (consumption >= limit)
                    {
                        //TODO: fire communication
                        TransactionLicenseLimitExceeded message = new TransactionLicenseLimitExceeded();
                        message.TransactionType = LicenseTransactionType.Trails.ToString();
                        message.UserId = user.Id;
                        message.AppContext= _flexAppContext;
                        //publish message
                        await _bus.Publish(message);
                        //packet.FlexServiceBusContext.Publish(message);

                        await _licenseService.LogLicenseViolationAsync(LicenseFeatureEnum.UserFeedbackLimit, limit, consumption, _flexAppContext);
                        packet.AddError("Error", $"Unable to submit transaction. The maximum allowed number of trails ({limit}) for the month has been reached. Please contact your administrator to increase the limit.");
                    }
                }
            }
        }
    }
}
