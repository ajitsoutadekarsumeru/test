using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule.UpdateTemplateStatusCommunicationPlugins
{
    public partial class ValidateTemplateStatus : FlexiBusinessRuleBase, IFlexiBusinessRule<UpdateTemplateStatusDataPacket>
    {
        public override string Id { get; set; } = "3a13373e7be65736efec636bfd4db343";
        public override string FriendlyName { get; set; } = "ValidateTemplateStatus";

        protected readonly ILogger<ValidateTemplateStatus> _logger;
        protected readonly IRepoFactory _repoFactory;

        public ValidateTemplateStatus(ILogger<ValidateTemplateStatus> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(UpdateTemplateStatusDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            await ValidateTemplateExistsAndActivationStatusAsync(packet);

            await ValidateTemplateDeactivationEligibilityAsync(packet);
        }

        private async Task ValidateTemplateExistsAndActivationStatusAsync(UpdateTemplateStatusDataPacket packet)
        {
            var template = await _repoFactory.GetRepo().FindAll<CommunicationTemplate>()
                                            .ByTFlexId(packet.Dto.TemplateId)
                                            .FirstOrDefaultAsync();
            if (template == null)
            {
                packet.AddError("Error", "Template does not exist");
            }
            else
            {
                //Check for invalid activation/deactivation requests based on the current template state
                bool isActive = !packet.Dto.IsDisabled;
                bool isDeactivated = packet.Dto.IsDisabled;

                if (isActive && template.IsActive)
                {
                    packet.AddError("Error", "Template is already enabled");
                }
                else if (isDeactivated && !template.IsActive)
                {
                    packet.AddError("Error", "Template is already disabled");
                }
            }
        }

        private async Task ValidateTemplateDeactivationEligibilityAsync(UpdateTemplateStatusDataPacket packet)
        {
            if (packet.Dto.IsDisabled)
            {
                //Checking if the template is associated with any active triggers
                List<string> mappedTemplates = await _repoFactory.GetRepo().FindAll<TriggerDeliverySpec>()
                                                    .ByTemplateId(packet.Dto.TemplateId)
                                                    .ByActiveTrigger()
                                                    .Select(s => s.CommunicationTrigger.Name)
                                                    .ToListAsync();
                if (mappedTemplates.Count > 0)
                {
                    packet.AddError("Error", $"This template is currently associated with the following active triggers: '{string.Join(",", mappedTemplates)}'");
                }
            }
        }
    }
}
