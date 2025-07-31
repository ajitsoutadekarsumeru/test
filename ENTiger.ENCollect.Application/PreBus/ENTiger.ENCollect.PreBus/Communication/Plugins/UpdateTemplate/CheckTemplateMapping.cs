using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule.UpdateTemplateCommunicationPlugins
{
    public partial class CheckTemplateMapping : FlexiBusinessRuleBase, IFlexiBusinessRule<UpdateTemplateDataPacket>
    {
        public override string Id { get; set; } = "3a13373c582611dae812e7a6a20b2640";
        public override string FriendlyName { get; set; } = "CheckTemplateDetails";

        protected readonly ILogger<CheckTemplateMapping> _logger;
        protected readonly IRepoFactory _repoFactory;

        public CheckTemplateMapping(ILogger<CheckTemplateMapping> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(UpdateTemplateDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            //Checking if the template is associated with any active triggers
            List<string> mappedTemplates = await _repoFactory.GetRepo().FindAll<TriggerDeliverySpec>()
                                                        .ByTemplateId(packet.Dto.Id)
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
