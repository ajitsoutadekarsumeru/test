using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class UpdateSettlementPlugin : FlexiPluginBase, IFlexiPlugin<UpdateSettlementPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19ff0f258bfa157478abee2c287643";
        public override string FriendlyName { get; set; } = "UpdateSettlementPlugin";
        
        protected string EventCondition = "";
        protected string _WorkflowInstanceId;
        protected readonly ILogger<UpdateSettlementPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;

        protected Settlement? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateSettlementPlugin(ILogger<UpdateSettlementPlugin> logger, IFlexHost flexHost, RepoFactory repoFactory)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
        }

        public virtual async Task Execute(UpdateSettlementPostBusDataPacket packet)
        {
            UpdateSettlementDto dto = packet.Cmd.Dto as UpdateSettlementDto;
            _flexAppContext = dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(_flexAppContext);

            var cmd = packet.Cmd;
            
            _WorkflowInstanceId = cmd.WorkflowInstanceId;

            _model = _repoFactory.GetRepo().FindAll<Settlement>()
                .Include(a=>a.WaiverDetails)
                .Include(b=>b.Installments)
                .Include(c=>c.Documents)
                .Where(m=>m.Id == dto.Id)
                .FirstOrDefault();
            
            if (_model != null)
            {
                _model.UpdateSettlement(cmd);
                _repoFactory.GetRepo().InsertOrUpdate(_model);

                int records = await _repoFactory.GetRepo().SaveAsync();
                if (records > 0)
                {
                    _logger.LogDebug("{Entity} with {EntityId} updated into Database: ", typeof(Settlement).Name, _model.Id);
                }
                else
                {
                    _logger.LogWarning("No records updated for {Entity} with {EntityId}", typeof(Settlement).Name, _model.Id);
                }

                // TODO: Specify your condition to raise event here...
                //TODO: Set the value of OnRaiseEventCondition according to your business logic

                //Example:
                EventCondition = CONDITION_ONSUCCESS;
            }
            else
            {
                //_logger.LogWarning("{Entity} with {EntityId} not found in Database: ", typeof(Settlement).Name, packet.Cmd.Dto.Id);

                //you may raise an event here to notify about the error
                //Example:
                //EventCondition = CONDITION_ONFAILED;
            }

 
            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}