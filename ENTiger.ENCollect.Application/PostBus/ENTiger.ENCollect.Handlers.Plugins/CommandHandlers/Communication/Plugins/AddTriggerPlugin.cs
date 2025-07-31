using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class AddTriggerPlugin : FlexiPluginBase, IFlexiPlugin<AddTriggerPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1a7123accd1299e3a656e779707b63";
        public override string FriendlyName { get; set; } = "AddTriggerPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<AddTriggerPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ITriggerTypeRepository _triggerTypeRepository;
        protected readonly ICommunicationTemplateRepository _templateRepository;

        protected TriggerType? _model;
        protected FlexAppContextBridge? _flexAppContext;

        public AddTriggerPlugin(ILogger<AddTriggerPlugin> logger, 
            IFlexHost flexHost,
            IRepoFactory repoFactory,
            ITriggerTypeRepository triggerTypeRepository,
            ICommunicationTemplateRepository templateRepository)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _triggerTypeRepository = triggerTypeRepository;
            _templateRepository = templateRepository;
        }

        public virtual async Task Execute(AddTriggerPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);
            var dto = packet.Cmd.Dto;

            _model = await _triggerTypeRepository.GetByTypeIdAsync(_flexAppContext, 
                packet.Cmd.Dto.TriggerTypeId); //TODO :: ById

            var trigger = new CommunicationTrigger(
            dto.Name,
            dto.DaysOffset,
            dto.Description,
            _flexAppContext.UserId,
            dto.RecipientType);

            //TODO :: MOve to Domain
            foreach (var templateId in dto.TriggerTemplates)
            {
                trigger.AssociateTemplate(templateId, _flexAppContext.UserId);
            }

            _model.AddTrigger(trigger);

            await _triggerTypeRepository.SaveAsync(_flexAppContext,_model);

            //TODO :: Event publish
        }
    }
}