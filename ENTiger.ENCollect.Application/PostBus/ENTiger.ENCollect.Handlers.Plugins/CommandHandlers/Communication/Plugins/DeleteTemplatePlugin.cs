using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class DeleteTemplatePlugin : FlexiPluginBase, IFlexiPlugin<DeleteTemplatePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13373f5176a7397dc76ac4f22993bb";
        public override string FriendlyName { get; set; } = "DeleteTemplatePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<DeleteTemplatePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CommunicationTemplate? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public DeleteTemplatePlugin(ILogger<DeleteTemplatePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(DeleteTemplatePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = await _repoFactory.GetRepo().FindAll<CommunicationTemplate>()
                        .FlexInclude(a => a.CommunicationTemplateDetails)
                        .Where(a => a.Id == packet.Cmd.Dto.Id).FirstOrDefaultAsync();

            if (_model != null)
            {
                _model.DeleteTemplate(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} deleted from Database: ", typeof(CommunicationTemplate).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records deleted for {Entity} with {EntityId}", typeof(CommunicationTemplate).Name, _model.Id);
                }

                //TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("CommunicationTemplate not found in Database: " + packet.Cmd.Dto.Id);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}