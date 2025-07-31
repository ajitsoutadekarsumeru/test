using ENTiger.ENCollect.AgencyModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ApproveCollectionCancellationPlugin : FlexiPluginBase, IFlexiPlugin<ApproveCollectionCancellationPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a136f5c941902ab71586931b6eecb2c";
        public override string FriendlyName { get; set; } = "ApproveCollectionCancellationPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<ApproveCollectionCancellationPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected List<Collection> collections;
        protected FlexAppContextBridge? _flexAppContext;

        protected readonly IDiffGenerator _diffGenerator;
        protected AuditEventData _auditData;
        protected List<Collection> collectionsDetails;
        public ApproveCollectionCancellationPlugin(ILogger<ApproveCollectionCancellationPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory, IDiffGenerator diffGenerator)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _diffGenerator = diffGenerator;
        }

        public virtual async Task Execute(ApproveCollectionCancellationPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            string userid = _flexAppContext.UserId;

            List<string> collectionIds = packet.Cmd.Dto.ReceiptIds.ToList();
            collectionsDetails = await _repoFactory.GetRepo().FindAll<Collection>().FlexInclude(y => y.CollectionWorkflowState)
                                                    .Where(x => collectionIds.Contains(x.Id)).FlexNoTracking().ToListAsync();

            await FetchCollections(collectionIds);
            foreach (Collection collection in collections)
            {
                collection.CancellationRemarks = packet.Cmd.Dto.Remarks;
                collection.Status = CollectionStatusEnum.cancelled.Value;
                collection.MarkAsCancelled(userid);

                _repoFactory.GetRepo().InsertOrUpdate(collection);
            }

            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ");

                await GenerateAndSendAuditEventAsync(packet);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}");
            }

            EventCondition = CONDITION_ONSUCCESS;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task GenerateAndSendAuditEventAsync(ApproveCollectionCancellationPostBusDataPacket packet)
        {
            foreach (Collection obj in collections)
            {
                Collection oldObject = collectionsDetails.Where(w => w.Id == obj.Id).FirstOrDefault();
                string jsonPatch = _diffGenerator.GenerateDiff(oldObject, obj);

                _auditData = new AuditEventData(
                                EntityId: obj?.Id,
                                EntityType: AuditedEntityTypeEnum.Collection.Value,
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

        private async Task FetchCollections(List<string> receiptIds)
        {
            _logger.LogDebug("CollectionCancellationApprovedCommandHandler : FetchCollections - Start");

            collections = await _repoFactory.GetRepo().FindAll<Collection>()
                                    .FlexInclude(y => y.CollectionWorkflowState)
                                    .Where(x => receiptIds.Contains(x.Id))
                                    .ToListAsync();

            _logger.LogInformation("CollectionCancellationApprovedCommandHandler : Collections - Count : " + collections.Count());
            _logger.LogDebug("CollectionCancellationApprovedCommandHandler : FetchCollections - End");
        }
    }
}