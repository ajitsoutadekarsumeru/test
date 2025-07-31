using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Workflows;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class ProcessSettlementCustomerAcceptancePlugin : FlexiPluginBase, IFlexiPlugin<ProcessSettlementCustomerAcceptancePostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19505791e262cee3b141609b465et1";
        public override string FriendlyName { get; set; } = "RequestSettlementPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<ProcessSettlementCustomerAcceptancePlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly RepoFactory _repoFactory;
        protected string _settlementId = "";
        protected string _WorkflowInstanceId;
        protected Settlement? _model;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly ISettlementRepository _settlementRepository;
        private readonly DynaWorkflowDefinition<IContextDataPacket> _wfDef;
        public ProcessSettlementCustomerAcceptancePlugin(ILogger<ProcessSettlementCustomerAcceptancePlugin> logger, 
            IFlexHost flexHost, RepoFactory repoFactory, ISettlementRepository settlementRepository )
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _settlementRepository = settlementRepository;
        }

        public virtual async Task Execute(ProcessSettlementCustomerAcceptancePostBusDataPacket packet)
        {
            UpdateStatusOfSettlementDto dto = packet.Cmd.Dto as UpdateStatusOfSettlementDto;
            _flexAppContext = dto.GetAppContext();
            _repoFactory.Init(_flexAppContext);

            var cmd = packet.Cmd;
            _settlementId = cmd.DomainId;
           
            var remarks = dto.Remarks?.Trim() ?? string.Empty;
            _WorkflowInstanceId = cmd.WorkflowInstanceId;

            // 1) Load the settlement, including its history
            var settlement = await _settlementRepository.GetByIdAsync(_flexAppContext, _settlementId);
            if (settlement == null) throw new InvalidOperationException("Settlement not found");

            // 2) Update Status
            settlement.ChangeStatus(SettlementStatusEnum.CustomerAcceptedOffer.Value,
                cmd.UserId, dto);

            //3) Save the settlement
            await _settlementRepository.SaveAsync(_flexAppContext, settlement);

            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}