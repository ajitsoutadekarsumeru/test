using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class AddTemplatePlugin : FlexiPluginBase, IFlexiPlugin<AddTemplatePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13373b8f169fc04765dd4cee8e8dee";
        public override string FriendlyName { get; set; } = "AddTemplatePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddTemplatePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CommunicationTemplate? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddTemplatePlugin(ILogger<AddTemplatePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddTemplatePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<CommunicationTemplate>().AddTemplate(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records == 0)
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(CommunicationTemplate).Name, _model.Id);
            }
            else
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(CommunicationTemplate).Name, _model.Id);
            }
        }
    }
}