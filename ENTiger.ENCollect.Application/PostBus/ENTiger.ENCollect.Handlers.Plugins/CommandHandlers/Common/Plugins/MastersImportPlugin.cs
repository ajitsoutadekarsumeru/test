using ENTiger.ENCollect.AgencyModule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class MastersImportPlugin : FlexiPluginBase, IFlexiPlugin<MastersImportPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13b884c2e5c9df4cafa242e352286a";
        public override string FriendlyName { get; set; } = "MastersImportPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<MastersImportPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;

        protected MasterFileStatus? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private List<string> staticHeaders = new List<string>();
        protected AuditEventData _auditData;

        private IFlexTenantRepository<FlexTenantBridge> _repoTenantFactory = FlexContainer.ServiceProvider.GetRequiredService<IFlexTenantRepository<FlexTenantBridge>>();

        public MastersImportPlugin(ILogger<MastersImportPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(MastersImportPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            string _filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);

            _model = _flexHost.GetDomainModel<MasterFileStatus>().MastersImport(packet.Cmd, _filepath);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records1 = await _repoFactory.GetRepo().SaveAsync();
            if (records1 > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(MasterFileStatus).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(MasterFileStatus).Name, _model.Id);
            }
            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(MastersImportPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.Master.Value,
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