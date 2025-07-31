using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.DomainModels.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule.AddCollectionCollectionsPlugins
{
    public partial class CheckUserPhysicalCollectionLimit : FlexiBusinessRuleBase, IFlexiBusinessRule<AddPhysicalCollectionDataPacket>
    {
        public override string Id { get; set; } = "6f5d4fab0e02421283c352a115889629";
        public override string FriendlyName { get; set; } = "CheckUserCollectionLimit";

        protected readonly ILogger<CheckUserPhysicalCollectionLimit> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ILicenseService _licenseService;
        protected readonly IFlexServiceBusBridge _bus;

        public CheckUserPhysicalCollectionLimit(ILogger<CheckUserPhysicalCollectionLimit> logger, IRepoFactory repoFactory, ILicenseService licenseService, IFlexServiceBusBridge bus)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _licenseService = licenseService;
            _bus = bus;
        }

        public virtual async Task Validate(AddPhysicalCollectionDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);
            _flexAppContext = packet.Dto.GetAppContext();  //do not remove this line
            string userid = _flexAppContext.UserId;
            //get the user detail
            try
            {
                ApplicationUser? user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                    .ByTFlexId(userid)
                    .FirstOrDefaultAsync();


                if (user != null)
                {
                    UserTypeEnum userType = Enum.TryParse(user.UserType, out UserTypeEnum result) ? result : UserTypeEnum.Unknown;
                    //check if user type is Others
                    if (userType == UserTypeEnum.Others)
                    {
                        var consumption = await _licenseService.GetFreeCollectionsConsumptionAsync(userid, packet.Dto);
                        var limit = _licenseService.GetFreeCollectionsLicenseLimit();
                        if (consumption >= limit)
                        {
                            TransactionLicenseLimitExceeded message = new TransactionLicenseLimitExceeded();
                            message.TransactionType = LicenseTransactionType.Collections.ToString();
                            message.UserId = user.Id;
                            message.AppContext = _flexAppContext;
                            //publish message
                            await _bus.Publish(message);

                            await _licenseService.LogLicenseViolationAsync(LicenseFeatureEnum.UserCollectionLimit, limit, consumption, _flexAppContext);
                            packet.AddError("Error", $"Unable to submit transaction. The maximum allowed number of collections ({limit}) for the month has been reached. Please contact your administrator to increase the limit.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
