using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Workflows;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class CancelSettlementPlugin : FlexiPluginBase, IFlexiPlugin<CancelSettlementPostBusDataPacket>
    {
        public override string Id { get; set; } = "5a19505791e262cee3b142609b465ef1";
        public override string FriendlyName { get; set; } = "CancelSettlementPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<CancelSettlementPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected string settlementId = "";
        protected string _stepName;
        protected string _stepType;
        protected string _WorkflowInstanceId;
        protected Settlement? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ISettlementRepository _settlementRepository;
        private readonly DynaWorkflowDefinition<IContextDataPacket> _wfDef;
        public CancelSettlementPlugin(
            ILogger<CancelSettlementPlugin> logger, 
            IFlexHost flexHost, 
            IRepoFactory repoFactory, 
            ISettlementRepository settlementRepository )
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
        }

        public virtual async Task Execute(CancelSettlementPostBusDataPacket packet)
        {
            CancelSettlementDto dto = packet.Cmd.Dto as CancelSettlementDto;
            _flexAppContext = dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(_flexAppContext);

            var cmd = packet.Cmd;
            settlementId = cmd.DomainId;

                      
            _WorkflowInstanceId = cmd.WorkflowInstanceId;
            _stepName = cmd.StepName;
            _stepType = cmd.StepType;

            // 1) Load the settlement, including its history
            _model = await _settlementRepository.GetByIdAsync(_flexAppContext, settlementId);
            if (_model == null) throw new InvalidOperationException("Settlement not found");

            // 2) Cancel 
            _model.ChangeStatus(SettlementStatusEnum.Cancelled.Value, cmd.UserId, cmd.CancelReason);

            //3) Save the settlement
            await _settlementRepository.SaveAsync(_flexAppContext, _model);


            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}