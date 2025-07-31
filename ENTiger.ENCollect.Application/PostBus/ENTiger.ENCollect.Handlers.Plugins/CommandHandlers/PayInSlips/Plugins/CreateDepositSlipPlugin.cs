using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class CreateDepositSlipPlugin : FlexiPluginBase, IFlexiPlugin<CreateDepositSlipPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138ab623e73669481b95e05bda73d0";
        public override string FriendlyName { get; set; } = "CreateDepositSlipPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<CreateDepositSlipPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ICustomUtility _customUtility;

        protected PayInSlip? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public CreateDepositSlipPlugin(ILogger<CreateDepositSlipPlugin> logger, 
            IFlexHost flexHost, 
            IRepoFactory repoFactory, 
            ICustomUtility customUtility)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
        }

        public virtual async Task Execute(CreateDepositSlipPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            if (string.IsNullOrEmpty(packet.Cmd.Dto.Id))
            {
                _model = _flexHost.GetDomainModel<PayInSlip>().CreateDepositSlip(packet.Cmd);
                List<CollectionBatch> batches = new List<CollectionBatch>();

                CollectionBatch collectionbatch = await CreateAutoBatch(packet.Cmd.Dto, _model.Id);
                batches.Add(collectionbatch);
                _model.CollectionBatches = new List<CollectionBatch>();
                _model.CollectionBatches = batches;
            }
            else
            {
                _model = await _repoFactory.GetRepo().FindAll<PayInSlip>().Where(x => x.Id == packet.Cmd.Dto.Id).FirstOrDefaultAsync();
                _model.SetLastModifiedBy(_flexAppContext.UserId);
                _model.PayInSlipImageName = packet.Cmd.Dto.PayInSlipImageName;
            }
            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                _logger.LogDebug("{Entity} with {EntityId} inserted into Database: ", typeof(PayInSlip).Name, _model.Id);
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId}", typeof(PayInSlip).Name, _model.Id);
            }

            // TODO: Specify your condition to raise event here...
            //TODO: Set the value of EventCondition according to your business logic
            if (string.IsNullOrEmpty(packet.Cmd.Dto.Id))
            {
                await this.Fire(EventCondition, packet.FlexServiceBusContext);
            }
            else
            {
                await Task.CompletedTask;
            }
        }

        private async Task<CollectionBatch> CreateAutoBatch(CreateDepositSlipDto model, string payInSlipId)
        {
            CollectionBatch collectionBatch = new CollectionBatch();
            collectionBatch.Amount = model.Amount;
            collectionBatch.CustomId = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.CollectionBatch.Value);
            collectionBatch.PayInSlipId = payInSlipId;
            collectionBatch.SetCreatedBy(_flexAppContext.UserId);
            collectionBatch.SetCreatedBy(collectionBatch.CreatedBy);
            collectionBatch.SetAddedOrModified();

            collectionBatch.CollectionBatchWorkflowState = _flexHost.GetFlexStateInstance<AddedCollectionBatchInPayInSlip>().SetTFlexId(collectionBatch.Id).SetStateChangedBy(collectionBatch.CreatedBy ?? "");

            List<Collection> Collections = new List<Collection>();
            Collections = await _repoFactory.GetRepo().FindAll<Collection>().Where(c => model.ReceiptIds.Contains(c.ReceiptId)).ToListAsync();
            foreach (Collection c in Collections)
            {
                c.CollectionWorkflowState = _flexHost.GetFlexStateInstance<AddedCollectionInBatch>().SetTFlexId(c.Id).SetStateChangedBy(collectionBatch.CreatedBy ?? "");
            }
            collectionBatch.Collections = new List<Collection>();
            collectionBatch.Collections = Collections;
            return collectionBatch;
        }
    }
}