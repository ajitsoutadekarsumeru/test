using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class UpdateUsersByBatchPlugin : FlexiPluginBase, IFlexiPlugin<UpdateUsersByBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139f5912932eefb7b15448a6aa49f7";
        public override string FriendlyName { get; set; } = "UpdateUsersByBatchPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateUsersByBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected UsersUpdateFile? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateUsersByBatchPlugin(ILogger<UpdateUsersByBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateUsersByBatchPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<UsersUpdateFile>().UpdateUsersByBatch(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(UsersUpdateFile).Name, _model.Id);
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(UsersUpdateFile).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}