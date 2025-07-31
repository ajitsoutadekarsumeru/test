using ENTiger.ENCollect.AgencyModule;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.IO.Abstractions;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountImportPlugin : FlexiPluginBase, IFlexiPlugin<AccountImportPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a13af91f74e0bb86301e31d3cdabf30";
        public override string FriendlyName { get; set; } = "AccountImportPlugin";

        protected string EventCondition = "";
        protected readonly ILogger<AccountImportPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected MasterFileStatus? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly FilePathSettings _fileSettings;
        private readonly IFileSystem _fileSystem;
        protected AuditEventData _auditData;
        public AccountImportPlugin(ILogger<AccountImportPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IOptions<FilePathSettings> fileSettings, IFileSystem fileSystem)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _fileSettings = fileSettings.Value;
            _fileSystem = fileSystem;
        }

        public virtual async Task Execute(AccountImportPostBusDataPacket packet)
        {
            _logger.LogInformation("AccountImportPlugin : Start");

            string _filepath = _fileSystem.Path.Combine(_fileSettings.BasePath, _fileSettings.IncomingPath);
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<MasterFileStatus>().AccountImport(packet.Cmd, _filepath);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int master_records = await _repoFactory.GetRepo().SaveAsync();
            if (master_records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(MasterFileStatus).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(MasterFileStatus).Name, _model.Id);
            }
            _logger.LogInformation("AccountImportPlugin : End | EventCondition - " + EventCondition);
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(AccountImportPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.Accounts.Value,
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