using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class UpdateTemplatePlugin : FlexiPluginBase, IFlexiPlugin<UpdateTemplatePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13373c8c29e95a9e48a9b425738cf5";
        public override string FriendlyName { get; set; } = "UpdateTemplatePlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateTemplatePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CommunicationTemplate? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected CommunicationTemplate? communicationTemplate;

        public UpdateTemplatePlugin(ILogger<UpdateTemplatePlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(UpdateTemplatePostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            communicationTemplate = await _repoFactory.GetRepo().FindAll<CommunicationTemplate>()
                                                .IncludeTemplateDetails()
                                                .Where(m => m.Id == packet.Cmd.Dto.Id)
                                                .FlexNoTracking()
                                                .FirstOrDefaultAsync();

            _model = await _repoFactory.GetRepo().FindAll<CommunicationTemplate>()
                                                .IncludeTemplateDetails()
                                                .Where(m => m.Id == packet.Cmd.Dto.Id)
                                                .FirstOrDefaultAsync();

            if (_model == null)
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CommunicationTemplate).Name, packet.Cmd.Dto.Id);
            }
            else
            {
                _model.UpdateTemplate(packet.Cmd, communicationTemplate);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records == 0)
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CommunicationTemplate).Name, _model.Id);
                }
                else
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CommunicationTemplate).Name, _model.Id);

                    await GenerateAndSendAuditEventAsync(packet);
                }
            }
        }

        private async Task GenerateAndSendAuditEventAsync(UpdateTemplatePostBusDataPacket packet)
        {
            string jsonPatch = _diffGenerator.GenerateDiff(communicationTemplate, _model);

            _auditData = new AuditEventData(
                            EntityId: _model?.Id,
                            EntityType: AuditedEntityTypeEnum.Communication.Value,
                            Operation: AuditOperationEnum.Edit.Value,
                            JsonPatch: jsonPatch,
                            InitiatorId: _flexAppContext?.UserId,
                            TenantId: _flexAppContext?.TenantId,
                            ClientIP: _flexAppContext?.ClientIP
                        );

            EventCondition = CONDITION_ONAUDITREQUEST;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}