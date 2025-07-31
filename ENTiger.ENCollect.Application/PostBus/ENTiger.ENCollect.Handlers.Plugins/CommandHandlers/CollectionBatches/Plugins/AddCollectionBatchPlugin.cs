using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionBatchesModule
{
    public partial class AddCollectionBatchPlugin : FlexiPluginBase, IFlexiPlugin<AddCollectionBatchPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138f2a8dc4a0ab7d7450fad1bb4c6c";
        public override string FriendlyName { get; set; } = "AddCollectionBatchPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<AddCollectionBatchPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected CollectionBatch? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private string userId;

        public AddCollectionBatchPlugin(ILogger<AddCollectionBatchPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(AddCollectionBatchPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            userId = _flexAppContext.UserId;

            List<Collection> collections;
            decimal amount;
            Decimal.TryParse(packet.Cmd.Dto.Amount.ToString(), out amount);
            collections = await GetCollectionsFromModelAsync(packet.Cmd.Dto);
            string CommisionerId = await getCommisionerIdAsync(userId);

            _model = _flexHost.GetDomainModel<CollectionBatch>().AddCollectionBatch(packet.Cmd);
            _model.SetAmount(amount);
            _model.SetModeOfPayment(packet.Cmd.Dto.ModeOfPayment);
            _model.Collections = collections;
            if (!String.IsNullOrEmpty(CommisionerId))
            {
                _model.CollectionBatchOrgId = CommisionerId;
            }

            foreach (Collection c in collections)
            {
                //  c.CollectionWorkflowState.AddCollectionInBatch(sc);
                c.CollectionBatchId = _model.Id;
                c.MarkAsAddedInBatch(userId);
                c.SetAddedOrModified();
            }

            _model.Collections = collections;
            _model.MarkAsCreated(userId);

            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(CollectionBatch).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(CollectionBatch).Name, _model.Id);
            }

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }

        private async Task<List<Collection>> GetCollectionsFromModelAsync(AddCollectionBatchDto model)
        {
            List<string> collectionIds = model.CollectionIds.ToList();

            List<Collection> batchCollections = await _repoFactory.GetRepo().FindAll<Collection>()
                                                    .Where(c => collectionIds.Contains(c.Id))
                                                    .FlexInclude(v => v.CollectionWorkflowState).ToListAsync();

            return batchCollections;
        }

        private async Task<string> getCommisionerIdAsync(string loggedInUserId)
        {
            return await _repoFactory.GetRepo().FindAll<Accountability>().Where(x => x.ResponsibleId == loggedInUserId)
                                            .Select(x => x.CommisionerId).FirstOrDefaultAsync();
        }
    }
}