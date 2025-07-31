using ENCollect.Dyna.Cascading;
using ENCollect.Dyna.Workflows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.SettlementModule
{
    public partial class RequestSettlementPlugin : FlexiPluginBase, IFlexiPlugin<RequestSettlementPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a19505791e262cee3b141609b465ef0";
        public override string FriendlyName { get; set; } = "RequestSettlementPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<RequestSettlementPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly RepoFactory _repoFactory;
        protected readonly ICustomUtility _customUtility;

        protected string _wfId = "";
        protected Settlement? _model;
        protected FlexAppContextBridge? _flexAppContext;

        
        private readonly DynaWorkflowDefinition<IContextDataPacket> _wfDef;
        public RequestSettlementPlugin(ILogger<RequestSettlementPlugin> logger, 
            IFlexHost flexHost, RepoFactory repoFactory, 
            ICustomUtility customUtility 
           )
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
            
        }

        public virtual async Task Execute(RequestSettlementPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            _model = _flexHost.GetDomainModel<Settlement>().RequestSettlement(packet.Cmd);


            _repoFactory.GetRepo().InsertOrUpdate(_model);
            int records = await _repoFactory.GetRepo().SaveAsync();
            if (records > 0)
            {
                var WorkflowName = "SettlementWorkflow"; //TODO appsettings
                _wfId = $"{_model.Id}_{WorkflowName}"; //WorkflowIdBuilder.BuildWorkflowInstanceId(_model.Id,_wfDef);
                _logger.LogInformation("{Entity} with {EntityId} inserted into Database ", typeof(Settlement).Name, _model.Id);
            }
            else
            {
                _logger.LogWarning("No records inserted for {Entity} with {EntityId} ", typeof(Settlement).Name, _model.Id);
            }
            
           
            EventCondition = CONDITION_ONSUCCESS;

            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}