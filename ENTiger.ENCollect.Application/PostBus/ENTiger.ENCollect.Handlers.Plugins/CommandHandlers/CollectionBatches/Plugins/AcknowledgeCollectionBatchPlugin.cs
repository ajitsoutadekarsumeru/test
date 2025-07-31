using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class AcknowledgeCollectionBatchPlugin : FlexiPluginBase, IFlexiPlugin<AcknowledgeCollectionBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138f2ba78e8276b26219bbe8ba4d74";
        public override string FriendlyName { get; set; } = "AcknowledgeCollectionBatchPlugin";

        protected string EventCondition = "";
        protected readonly ILogger<AcknowledgeCollectionBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CollectionBatch? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected CollectionBatch? collectionBatch;

        public AcknowledgeCollectionBatchPlugin(ILogger<AcknowledgeCollectionBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(AcknowledgeCollectionBatchPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var input = packet.Cmd.Dto;
            string userId = _flexAppContext.UserId;
            string batchId = input.batchId;

            collectionBatch = await _repoFactory.GetRepo().FindAll<CollectionBatch>().Where(m => m.Id == packet.Cmd.Dto.batchId)
                                        .IncludeCollectionBatchWorkflow().FlexNoTracking().FirstOrDefaultAsync();

            _model = await _repoFactory.GetRepo().FindAll<CollectionBatch>().Where(m => m.Id == packet.Cmd.Dto.batchId).FirstOrDefaultAsync();

            if (_model != null)
            {
                _model.AcknowledgedById = userId;
                _model.AcknowledgeCollectionBatch(packet.Cmd, userId);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CollectionBatch).Name, _model.Id);

                    await GenerateAndSendAuditEventAsync(packet);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CollectionBatch).Name, _model.Id);
                }
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CollectionBatch).Name, packet.Cmd.Dto.batchId);
            }
        }

        private async Task GenerateAndSendAuditEventAsync(AcknowledgeCollectionBatchPostBusDataPacket packet)
        {
            string jsonPatch = _diffGenerator.GenerateDiff(collectionBatch, _model);

            _auditData = new AuditEventData(
                            EntityId: _model?.Id,
                            EntityType: AuditedEntityTypeEnum.CollectionBatch.Value,
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