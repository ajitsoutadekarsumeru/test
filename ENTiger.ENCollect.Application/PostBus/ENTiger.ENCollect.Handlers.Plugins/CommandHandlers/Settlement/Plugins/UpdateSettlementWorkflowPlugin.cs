using ENCollect.Dyna.Workflows;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class UpdateSettlementWorkflowPlugin : FlexiPluginBase, IFlexiPlugin<UpdateSettlementPostBusDataPacket>
    {
        public override string Id { get; set; } = "4a19ff0f258bfa157478abee2c287643";
        public override string FriendlyName { get; set; } = "UpdateSettlementPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<UpdateSettlementWorkflowPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexServiceBusBridge _bus;
        protected readonly ISettlementRepository _settlementRepository;

        protected Settlement? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateSettlementWorkflowPlugin(
            ILogger<UpdateSettlementWorkflowPlugin> logger, 
            IFlexHost flexHost, 
            IRepoFactory repoFactory,
            ISettlementRepository settlementRepository,
            IFlexServiceBusBridge bus)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _bus = bus;
            _settlementRepository = settlementRepository;
        }

        public virtual async Task Execute(UpdateSettlementPostBusDataPacket packet)
        {
            UpdateSettlementDto dto = packet.Cmd.Dto as UpdateSettlementDto;
            _flexAppContext = dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(_flexAppContext);
            string userId = _flexAppContext.UserId;
            
            
            var queueProjection = _settlementRepository.GetQueueProjectionBySettlementIdAsync(_flexAppContext, dto.Id, userId).Result;

            DynaWorkflowTransitionCommand command = new DynaWorkflowTransitionCommand();
            command.FlexAppContext = _flexAppContext;
            command.WorkflowInstanceId = queueProjection.WorkflowInstanceId;
            command.StepName = queueProjection.StepName;
            command.UserId = userId;
            command.Dto = dto;
            command.ActionType = ActionType.Update;

            await _bus.Send(command);

        }
    }
}