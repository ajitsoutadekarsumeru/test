using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule.AddTemplateCommunicationPlugins
{
    public partial class CheckForDuplicateTemplate : FlexiBusinessRuleBase, IFlexiBusinessRule<AddTemplateDataPacket>
    {
        public override string Id { get; set; } = "3a13373b553fef712fb047d1bac4ad57";
        public override string FriendlyName { get; set; } = "CheckForDuplicateTemplate";

        protected readonly ILogger<CheckForDuplicateTemplate> _logger;
        protected readonly IRepoFactory _repoFactory;

        public CheckForDuplicateTemplate(ILogger<CheckForDuplicateTemplate> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddTemplateDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            //Check if a template with the same name already exists to prevent duplicates
            var TemplateExists = await _repoFactory.GetRepo().FindAll<CommunicationTemplate>()
                                            .ByTemplateName(packet.Dto.Name)
                                            .FirstOrDefaultAsync();
            if (TemplateExists != null)
            {
                packet.AddError("Error", "A template with same name already exists. Please use another name.");
            }
        }
    }
}
