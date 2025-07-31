using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class VerifyLoginOTPPlugin : FlexiPluginBase, IFlexiPlugin<VerifyLoginOTPPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a134c68dd1bc33cb74e6f7afd35d914";
        public override string FriendlyName { get; set; } = "VerifyLoginOTPPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<VerifyLoginOTPPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected ApplicationUser? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public VerifyLoginOTPPlugin(ILogger<VerifyLoginOTPPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(VerifyLoginOTPPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<ApplicationUser>().VerifyLoginOTP(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(ApplicationUser).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(ApplicationUser).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}