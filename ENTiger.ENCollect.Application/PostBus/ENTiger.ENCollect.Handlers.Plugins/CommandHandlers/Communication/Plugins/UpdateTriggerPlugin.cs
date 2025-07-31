using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Linq;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule
{
    public partial class UpdateTriggerPlugin : FlexiPluginBase, IFlexiPlugin<UpdateTriggerPostBusDataPacket>
    {
        public override string Id { get; set; } = "3a1a7124394a933a68e4abef67a6ddf1";
        public override string FriendlyName { get; set; } = "UpdateTriggerPlugin";
        
        protected string EventCondition = "";

        protected readonly ILogger<UpdateTriggerPlugin> _logger;
        protected readonly IFlexHost _flexHost;
        protected readonly IRepoFactory _repoFactory;
        protected readonly ITriggerTypeRepository _triggerTypeRepository;
        protected readonly ICommunicationTemplateRepository _templateRepository;

        protected TriggerType? _triggerType;
        protected FlexAppContextBridge? _flexAppContext;

        public UpdateTriggerPlugin(ILogger<UpdateTriggerPlugin> logger, 
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

        public virtual async Task Execute(UpdateTriggerPostBusDataPacket packet)
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

                // // 3. Remove/disassociate all existing templates
                if (trigger != null)
                {
                    // 4. Remove all existing templates
                    foreach (var template in trigger.TriggerTemplates.ToList())
                    {
                        trigger.DissociateTemplate(template.Id);
                    }

                    // 5. Associate new templates
                    foreach (var templateId in dto.TriggerTemplates)
                    {
                        trigger.AssociateTemplate(templateId, _flexAppContext.UserId);
                    }

                    // update the RecipientType
                    trigger.UpdateTrigger(packet.Cmd);

                    // 6. Save the updated TriggerType
                    await _triggerTypeRepository.SaveAsync(_flexAppContext, _triggerType);
                }
                else
                {
                    throw new InvalidOperationException("Trigger not found.");
                }
            }

            //EventCondition = CONDITION_ONSUCCESS;
            //await this.Fire(EventCondition, packet.FlexServiceBusContext);

        }
    }
}