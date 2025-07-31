using Elastic.Clients.Elasticsearch.Security;
using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Workflows;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class RecordPotentialActorsPlugin : FlexiPluginBase, IFlexiPlugin<RecordPotentialActorsPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19505791e262cee3b141609b465ef1";
        public override string FriendlyName { get; set; } = "RequestSettlementPlugin";
        
        protected string EventCondition = "";
        protected List<string> _actors;
        protected readonly ILogger<RecordPotentialActorsPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly RepoFactory _repoFactory;
        protected string _workflowInstanceId = "";
        protected string? _settlementId;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ISettlementRepository _settlementRepository;
        private readonly DynaWorkflowDefinition<IContextDataPacket> _wfDef;
        public RecordPotentialActorsPlugin(ILogger<RecordPotentialActorsPlugin> logger, 
            IFlexHost flexHost, RepoFactory repoFactory, ISettlementRepository settlementRepository)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
        }

        public virtual async Task Execute(RecordPotentialActorsPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.FlexAppContext;  //do not remove this line
            _repoFactory.Init(_flexAppContext);

            _settlementId = packet.Cmd.DomainId;

            _workflowInstanceId = packet.Cmd.WorkflowInstanceId;
            var workflowName = packet.Cmd.WorkflowName;           
            _actors = packet.Cmd.EligibleUserIds;

            // 1) Delete existing projection rows for this workflow instance
            var queueProjections = await _settlementRepository.GetQueueProjectionsByWorkflowInstanceId(_flexAppContext,_workflowInstanceId);
            foreach(var queueItem in queueProjections)
            {
                queueItem.Delete();
            }

            // 2) Create new projection entries
            foreach (var actor in _actors)
            {
                // Use the constructor taking raw GUIDs to avoid extra lookups
                var queueProjection = new SettlementQueueProjection(
                    workflowName: workflowName,
                    workflowInstanceId: _workflowInstanceId,
                    settlementId: _settlementId,                   
                    userId: actor,
                    stepName: packet.Cmd.StepName,
                    stepType: packet.Cmd.UIActionContext,
                    uIActionContext: packet.Cmd.UIActionContext
                );

                queueProjections.Add(queueProjection);
            }


            //3) Update the Projection
            foreach (var obj in queueProjections)
            {                
                _repoFactory.GetRepo().InsertOrUpdate(obj);
            }

            int records = await _repoFactory.GetRepo().SaveAsync();

            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}