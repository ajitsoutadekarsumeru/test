using ENTiger.ENCollect.AgencyModule;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionBulkUploadPlugin : FlexiPluginBase, IFlexiPlugin<CollectionBulkUploadPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a182aa8138c059659b43677718595b3";
        public override string FriendlyName { get; set; } = "CollectionBulkUploadPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<CollectionBulkUploadPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CollectionUploadFile? _model;
        protected FlexAppContextBridge? _flexAppContext;

        protected AuditEventData _auditData;
        public CollectionBulkUploadPlugin(
            ILogger<CollectionBulkUploadPlugin> logger, 
            IFlexHost flexHost, 
            IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CollectionBulkUploadPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<CollectionUploadFile>().CollectionBulkUpload(packet.Cmd);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(CollectionUploadFile).Name, _model.Id);

                await GenerateAndSendAuditEventAsync(packet);

                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(CollectionUploadFile).Name, _model.Id);
            }
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(CollectionBulkUploadPostBusDataPacket packet)
        {
            string jsonPatch = JsonConvert.SerializeObject(_model);

            _auditData = new AuditEventData(
                EntityId: _model?.Id,
                EntityType: AuditedEntityTypeEnum.Collection.Value,
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