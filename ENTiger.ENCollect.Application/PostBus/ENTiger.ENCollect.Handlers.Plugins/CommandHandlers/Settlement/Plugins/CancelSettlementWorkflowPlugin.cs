using ENCollect.Dyna.Workflows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class CancelSettlementWorkflowPlugin : FlexiPluginBase, IFlexiPlugin<CancelSettlementPostBusDataPacket>
    {
        public override string Id { get; set; } = "6a19ff0f258bfa157478abee2c287643";
        public override string FriendlyName { get; set; } = "CancelSettlementWorkflowPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<CancelSettlementWorkflowPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly IFlexServiceBusBridge _bus;

        protected Settlement? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public CancelSettlementWorkflowPlugin(
            ILogger<CancelSettlementWorkflowPlugin> logger, 
            IFlexHost flexHost, 
            IRepoFactory repoFactory,
            IFlexServiceBusBridge bus)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _bus = bus;
        }

        public virtual async Task Execute(CancelSettlementPostBusDataPacket packet)
        {
            CancelSettlementDto dto = packet.Cmd.Dto as CancelSettlementDto;
            _flexAppContext = dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(_flexAppContext);

            var settlements = await _repoFactory.GetRepo().FindAll<Settlement>()
               .Where(x => dto.Ids.Contains(x.Id) 
               && x.CreatedBy == _flexAppContext.UserId
               && x.IsDeleted == false)
               .Select(x => new {
                   x.Id,
                   x.QueueProjections                  
               })
               .ToListAsync();


            foreach (var settlement in settlements)
            {
                _logger.LogInformation($"Canceling settlement with ID: {settlement.Id}");

                var queueProjection = settlement.QueueProjections.Where(a=>a.IsDeleted == false).FirstOrDefault();

                DynaWorkflowTransitionCommand command = new DynaWorkflowTransitionCommand();
                command.FlexAppContext = _flexAppContext;
                command.WorkflowInstanceId = queueProjection.WorkflowInstanceId;
                command.StepName = queueProjection.StepName;
                command.UserId = _flexAppContext.UserId;
                command.Dto = packet.Cmd.Dto;
                command.ActionType = ActionType.Cancel;

                await _bus.Send(command);
            }

           
        }
    }
}