using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.HierarchyModule
{
    public partial class AddGeoMasterPlugin : FlexiPluginBase, IFlexiPlugin<AddGeoMasterPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1afc5d228640899176cdbd5c28f9b2";
        public override string FriendlyName { get; set; } = "AddGeoMasterPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<AddGeoMasterPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly RepoFactory _repoFactory;

        protected HierarchyMaster? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddGeoMasterPlugin(ILogger<AddGeoMasterPlugin> logger, IFlexHost flexHost, RepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddGeoMasterPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetPersistenceModel<HierarchyMaster>().AddGeoMaster(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records <= 0)
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(HierarchyMaster).Name, _model.Id); 
            }
            else
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(HierarchyMaster).Name, _model.Id);
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}