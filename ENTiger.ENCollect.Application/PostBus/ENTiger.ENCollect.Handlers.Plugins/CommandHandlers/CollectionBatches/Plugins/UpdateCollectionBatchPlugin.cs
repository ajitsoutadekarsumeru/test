using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class UpdateCollectionBatchPlugin : FlexiPluginBase, IFlexiPlugin<UpdateCollectionBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138f2ca156cb0560704db7d15d5f96";
        public override string FriendlyName { get; set; } = "UpdateCollectionBatchPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<UpdateCollectionBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CollectionBatch? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private string userid;

        public UpdateCollectionBatchPlugin(ILogger<UpdateCollectionBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateCollectionBatchPostBusDataPacket packet)
        {
            List<Collection> collectionList;
            int batchCollectionCount;
            int editCollectionCount;
            decimal? totalAmount = 0;
            decimal? amount = 0;

            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            userid = _flexAppContext.UserId;

            var model = packet.Cmd.Dto;
            _model = await FetchCollectionBatchAsync(model.BatchId);

            collectionList = (await FetchCollectionAsync(model.BatchId)).Where(u => model.CollectionIds.Contains(u.Id)).ToList();
            editCollectionCount = collectionList.Count();

            foreach (Collection collection in collectionList)
            {
                collection.CollectionBatch = null;
                collection.CollectionBatchId = null;
                totalAmount = totalAmount + collection.Amount ?? 0;

                _logger.LogDebug("Collection AckingAgentId " + collection.AckingAgentId);
                if (collection.AckingAgentId == null || collection.AckingAgentId == "")
                {
                    collection.MarkAsReceivedByCollector(userid);
                    _logger.LogDebug("Collection AckStatus " + collection.CollectionWorkflowState.GetType().Name);
                }
                else
                {
                    collection.MarkAsReadyForBatch(userid);
                    _logger.LogDebug("Collection Status " + collection.CollectionWorkflowState.GetType().Name);
                }

                amount = _model.Amount - totalAmount;
                _model.SetAmount(amount);
                _model.SetUpdatedCollectionState(editCollectionCount, userid);
            }

            if (_model != null)
            {
                _model.UpdateCollectionBatch(packet.Cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(CollectionBatch).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(CollectionBatch).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                //EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(CollectionBatch).Name, packet.Cmd.Dto.BatchId);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<List<Collection>> FetchCollectionAsync(string batchId)
        {
            return await _repoFactory.GetRepo().FindAll<Collection>().Where(c => c.CollectionBatchId == batchId).ToListAsync();
        }

        private async Task<CollectionBatch?> FetchCollectionBatchAsync(string batchId)
        {
            return await _repoFactory.GetRepo().Find<CollectionBatch>(batchId)
                              .FlexInclude(c => c.CollectionBatchWorkflowState)
                              .FlexInclude(c => c.Collections)
                              .FirstOrDefaultAsync();
        }
    }
}