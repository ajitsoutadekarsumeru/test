using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Workflows;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementApprovalPlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementApprovalPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19505791e262cee3b141609b465ef1";
        public override string FriendlyName { get; set; } = "RequestSettlementPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<ProcessSettlementApprovalPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly RepoFactory _repoFactory;
        protected string settlementId = "";
        protected string _WorkflowInstanceId;
        protected Settlement? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ISettlementRepository _settlementRepository;
        private readonly DynaWorkflowDefinition<IContextDataPacket> _wfDef;
        public ProcessSettlementApprovalPlugin(ILogger<ProcessSettlementApprovalPlugin> logger, 
            IFlexHost flexHost, RepoFactory repoFactory, ISettlementRepository settlementRepository )
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
        }

        public virtual async Task Execute(ProcessSettlementApprovalPostBusDataPacket packet)
        {
            UpdateStatusOfSettlementDto dto = packet.Cmd.Dto as UpdateStatusOfSettlementDto;
            _flexAppContext = dto.GetAppContext();  
            _repoFactory.Init(_flexAppContext);

            var cmd = packet.Cmd;
            settlementId = cmd.DomainId;            

           
            var remarks = dto.Remarks?.Trim() ?? string.Empty;
            _WorkflowInstanceId = cmd.WorkflowInstanceId;

            // 1) Load the settlement, including its history
            _model = await _settlementRepository.GetByIdAsync(_flexAppContext, settlementId);
            if (_model == null) throw new InvalidOperationException("Settlement not found");

            // 2) Update Status
            _model.ChangeStatus(SettlementStatusEnum.PendingCustomerAcceptance.Value, 
                cmd.UserId, dto);

            //3) Save the settlement
            await _settlementRepository.SaveAsync(_flexAppContext, _model);


            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}