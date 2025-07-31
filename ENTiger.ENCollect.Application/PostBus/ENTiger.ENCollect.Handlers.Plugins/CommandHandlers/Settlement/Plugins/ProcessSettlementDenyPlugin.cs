using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Workflows;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementDenyPlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementDenyPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19505791e262cee3b141609b465ef1";
        public override string FriendlyName { get; set; } = "RequestSettlementPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<ProcessSettlementDenyPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly RepoFactory _repoFactory;
        protected string _stepName;
        protected string _stepType;
        protected string _WorkflowInstanceId;
        protected Settlement? _settlement;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ISettlementRepository _settlementRepository;
        private readonly DynaWorkflowDefinition<IContextDataPacket> _wfDef;
        public ProcessSettlementDenyPlugin(ILogger<ProcessSettlementDenyPlugin> logger, 
            IFlexHost flexHost, RepoFactory repoFactory, ISettlementRepository settlementRepository )
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
        }

        public virtual async Task Execute(ProcessSettlementDenyPostBusDataPacket packet)
        {
            UpdateStatusOfSettlementDto dto = packet.Cmd.Dto as UpdateStatusOfSettlementDto;
            _flexAppContext = dto.GetAppContext();
            _repoFactory.Init(_flexAppContext);

            var cmd = packet.Cmd;
            var settlementId = cmd.DomainId;
                       

            var remarks = dto.Remarks?.Trim() ?? string.Empty;
            var rejectionReason = dto.RejectionReason?.Trim() ?? string.Empty;
            _WorkflowInstanceId = cmd.WorkflowInstanceId;
            _stepName = cmd.StepName;
            _stepType = cmd.StepType;


            // 1) Load the settlement, including its history
            _settlement = await _settlementRepository.GetByIdAsync(_flexAppContext, settlementId);
            if (_settlement == null) throw new InvalidOperationException("Settlement not found");

            // 2) Update Status
            _settlement.ChangeStatus(SettlementStatusEnum.RequestRejected.Value, 
                cmd.UserId, dto);

            //3) Save the settlement
            await _settlementRepository.SaveAsync(_flexAppContext, _settlement);


            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}