using ENTiger.ENCollect.AgencyModule;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class PrimaryUnAllocationByBatchPlugin : FlexiPluginBase, IFlexiPlugin<PrimaryUnAllocationByBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139fef00dbc43aee57bc4119b9cd2f";
        public override string FriendlyName { get; set; } = "PrimaryUnAllocationByBatchPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<PrimaryUnAllocationByBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected PrimaryUnAllocationFile? _model;
        protected FlexAppContextBridge? _flexAppContext;
        protected string? _unAllocationType;

        protected AuditEventData _auditData;

        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;

        public PrimaryUnAllocationByBatchPlugin(ILogger<PrimaryUnAllocationByBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory
                , IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(PrimaryUnAllocationByBatchPostBusDataPacket packet)
        {
            _logger.LogInformation("PrimaryUnAllocationByBatchPlugin : Start");
            string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);

            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _unAllocationType = packet.Cmd.Dto.UnAllocationType;

            _model = _flexHost.GetDomainModel<PrimaryUnAllocationFile>().PrimaryUnAllocationByBatch(packet.Cmd, filePath);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(PrimaryUnAllocationFile).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(PrimaryUnAllocationFile).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(PrimaryUnAllocationByBatchPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);
            _auditData = new AuditEventData(
                            EntityId: _model?.Id,
                            EntityType: AuditedEntityTypeEnum.PrimaryBulkDeAllocation.Value,
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