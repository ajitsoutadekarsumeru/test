using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.DevicesModule
{
    public partial class VerifyRegisteredDevicePlugin : FlexiPluginBase, IFlexiPlugin<VerifyRegisteredDevicePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a131f3e0afe4f56a80df67fe0733477";
        public override string FriendlyName { get; set; } = "VerifyRegisteredDevicePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<VerifyRegisteredDevicePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected DeviceDetail? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public VerifyRegisteredDevicePlugin(ILogger<VerifyRegisteredDevicePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(VerifyRegisteredDevicePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<DeviceDetail>().VerifyRegisteredDevice(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(DeviceDetail).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(DeviceDetail).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic

            //Example:
            //EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}