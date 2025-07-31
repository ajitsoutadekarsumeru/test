using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class VerifyAddNumberOTPPlugin : FlexiPluginBase, IFlexiPlugin<VerifyAddNumberOTPPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a134d0f0f05ca6f2bb50a8116993be9";
        public override string FriendlyName { get; set; } = "VerifyAddNumberOTPPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<VerifyAddNumberOTPPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected UserVerificationCodes? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public VerifyAddNumberOTPPlugin(ILogger<VerifyAddNumberOTPPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(VerifyAddNumberOTPPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<UserVerificationCodes>().VerifyAddNumberOTP(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(UserVerificationCodes).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(UserVerificationCodes).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}