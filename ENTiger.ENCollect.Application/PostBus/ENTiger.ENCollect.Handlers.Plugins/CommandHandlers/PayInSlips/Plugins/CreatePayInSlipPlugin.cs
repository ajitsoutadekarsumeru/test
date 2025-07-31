using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class CreatePayInSlipPlugin : FlexiPluginBase, IFlexiPlugin<CreatePayInSlipPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a138651b79c46b7d5b249c2482dafb4";
        public override string FriendlyName { get; set; } = "CreatePayInSlipPlugin";

        protected string EventCondition = "";

        protected readonly ILogger<CreatePayInSlipPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected PayInSlip? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public CreatePayInSlipPlugin(ILogger<CreatePayInSlipPlugin> logger, IFlexHost flexHost, IRepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(CreatePayInSlipPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<PayInSlip>().CreatePayInSlip(packet.Cmd);

            List<CollectionBatch> batches = await _repoFactory.GetRepo().FindAll<CollectionBatch>().ByIds(packet.Cmd.Dto.BatchIds).ToListAsync();
            foreach (var obj in batches)
            {
                obj.PayInSlipId = _model.Id;
                obj.CollectionBatchWorkflowState = _flexHost.GetFlexStateInstance<AddedCollectionBatchInPayInSlip>().SetTFlexId(obj.Id).SetStateChangedBy(_model.CreatedBy ?? "");
                obj.SetAddedOrModified();
            }
            _model.CollectionBatches = batches;
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

            await this.Fire(EventCondition, packet.FlexServiceBusContext);
        }
    }
}