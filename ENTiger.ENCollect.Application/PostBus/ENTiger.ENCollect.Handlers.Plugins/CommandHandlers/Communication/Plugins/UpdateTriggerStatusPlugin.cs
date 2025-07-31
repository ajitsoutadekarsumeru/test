using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class UpdateTriggerStatusPlugin : FlexiPluginBase, IFlexiPlugin<UpdateTriggerStatusPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1a7125247dc4a327cf593a0dccf6c1";
        public override string FriendlyName { get; set; } = "UpdateTriggerStatusPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<UpdateTriggerStatusPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ITriggerTypeRepository _triggerTypeRepository;
        protected readonly ICommunicationTemplateRepository _templateRepository;

        protected TriggerType? _triggerType;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateTriggerStatusPlugin(ILogger<UpdateTriggerStatusPlugin> logger, 
            IFlexHost flexHost, 
            IRepoFactory repoFactory,
            ICommunicationTemplateRepository templateRepository,
            ITriggerTypeRepository triggerTypeRepository)
        {
            _logger = logger;
            _flexHost = flexHost;
            _repoFactory = repoFactory;
            _triggerTypeRepository = triggerTypeRepository;
            _templateRepository = templateRepository;
        }

        public virtual async Task Execute(UpdateTriggerStatusPostBusDataPacket packet)
        {
            _flexAppContext = packet.Cmd.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Cmd.Dto);

            var dto = packet.Cmd.Dto;

            // 1. Load the aggregate based on the TriggerTypeId
            _triggerType = await _triggerTypeRepository.GetByTypeIdAsync(_flexAppContext, 
                packet.Cmd.Dto.TriggerTypeId);

            if (_triggerType != null)
            {
                // 2. Find the trigger
                var trigger = _triggerType.Triggers.FirstOrDefault(t => t.Id == dto.Id);

                //3. Check if the trigger exists
                if (trigger != null)
                {
                    // 4. Invoke domain behavior
                    if (dto.IsActive)
                        trigger.Enable();
                    else
                        trigger.Disable();

                    trigger.SetAddedOrModified();
                    // 5. Save the updated TriggerType
                    await _triggerTypeRepository.SaveAsync(_flexAppContext, _triggerType);
                }
                else
                {
                    throw new InvalidOperationException("Trigger not found.");
                }
            }

            //EventCondition = CONDITION_ONSUCCESS;
            await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}