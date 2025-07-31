using ENTiger.ENCollect.AgencyModule;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class PrimaryAllocationByBatchPlugin : FlexiPluginBase, IFlexiPlugin<PrimaryAllocationByBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a139de212d0ae1b5ac6f194021660fe";
        public override string FriendlyName { get; set; } = "PrimaryAllocationByBatchPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<PrimaryAllocationByBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected PrimaryAllocationFile? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private string? loggedInUserId;

        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;

        protected AuditEventData _auditData;
        public PrimaryAllocationByBatchPlugin(ILogger<PrimaryAllocationByBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory
                , IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(PrimaryAllocationByBatchPostBusDataPacket packet)
        {
            _logger.LogInformation("PrimaryAllocationByBatchPlugin : Start");
            string filePath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<PrimaryAllocationFile>().PrimaryAllocationByBatch(packet.Cmd, filePath);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(PrimaryAllocationFile).Name, _model.Id);
                await GenerateAndSendAuditEventAsync(packet);
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(PrimaryAllocationFile).Name, _model.Id);
            }
            _logger.LogInformation("PrimaryAllocationByBatchPlugin : End | EventCondition - " + EventCondition);
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(PrimaryAllocationByBatchPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.PrimaryBulkAllocation.Value,
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