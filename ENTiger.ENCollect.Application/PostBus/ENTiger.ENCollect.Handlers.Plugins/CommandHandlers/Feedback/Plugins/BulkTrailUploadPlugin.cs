using ENTiger.ENCollect.AgencyModule;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class BulkTrailUploadPlugin : FlexiPluginBase, IFlexiPlugin<BulkTrailUploadPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13b3ef0c9f3a5c673a7b5a938c3566";
        public override string FriendlyName { get; set; } = "BulkTrailUploadPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<BulkTrailUploadPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected BulkTrailUploadFile? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected AuditEventData _auditData;

        public BulkTrailUploadPlugin(ILogger<BulkTrailUploadPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(BulkTrailUploadPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var inputmodel = packet.Cmd.Dto;

            _model = _flexHost.GetDomainModel<BulkTrailUploadFile>().BulkTrailUpload(packet.Cmd);
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(BulkTrailUploadFile).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(BulkTrailUploadFile).Name, _model.Id);
            }

            EventCondition = CONDITION_ONSUCCESS;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(BulkTrailUploadPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.Trails.Value,
                Operation: AuditOperationEnum.Upload.Value,
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