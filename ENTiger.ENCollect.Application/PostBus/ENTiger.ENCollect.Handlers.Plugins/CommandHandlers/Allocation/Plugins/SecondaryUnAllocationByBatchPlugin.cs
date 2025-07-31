using ENTiger.ENCollect.AgencyModule;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SecondaryUnAllocationByBatchPlugin : FlexiPluginBase, IFlexiPlugin<SecondaryUnAllocationByBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139ff0b4ad06a055cfb78fb431674f";
        public override string FriendlyName { get; set; } = "SecondaryUnAllocationByBatchPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<SecondaryUnAllocationByBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected SecondaryUnAllocationFile? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected string _unAllocationType;

        protected AuditEventData _auditData;

        public SecondaryUnAllocationByBatchPlugin(ILogger<SecondaryUnAllocationByBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(SecondaryUnAllocationByBatchPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            _unAllocationType = packet.Cmd.Dto.UnAllocationType;

            _model = _flexHost.GetDomainModel<SecondaryUnAllocationFile>().SecondaryUnAllocationByBatch(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(SecondaryUnAllocationFile).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(SecondaryUnAllocationFile).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(SecondaryUnAllocationByBatchPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.SecondaryBulkDeAllocation.Value,
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