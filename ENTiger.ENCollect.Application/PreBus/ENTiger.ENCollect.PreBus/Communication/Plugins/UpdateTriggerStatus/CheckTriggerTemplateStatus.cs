using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CommunicationModule.AddTriggerCommunicationPlugins
{
    public partial class CheckTriggerTemplateStatus : FlexiBusinessRuleBase, IFlexiBusinessRule<UpdateTriggerStatusDataPacket>
    {
        public override string Id { get; set; } = "207810d5486611f0932c00ff9033f9ef";
        public override string FriendlyName { get; set; } = "CheckTriggerTemplateStatus";

        protected readonly ILogger<CheckTriggerTemplateStatus> _logger;
        protected readonly RepoFactory _repoFactory;

        public CheckTriggerTemplateStatus(ILogger<CheckTriggerTemplateStatus> logger, RepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(UpdateTriggerStatusDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            if (packet.Dto.IsActive == true)
            {
                var disabledTemplateNamesList = await _repoFactory.GetRepo().FindAll<CommunicationTrigger>()
                                                            .ByTriggersId(packet.Dto.Id)
                                                            .SelectMany(trigger => trigger.TriggerTemplates)
                                                            .Where(w => w.CommunicationTemplate.IsActive == false)
                                                            .Select(t => t.CommunicationTemplate.Name)
                                                            .ToListAsync();
                string disabledTemplateNames = string.Join(",", disabledTemplateNamesList);
                
                if (!string.IsNullOrWhiteSpace(disabledTemplateNames))
                {
                    packet.AddError("Error", $"Attached templates '{disabledTemplateNames}' is disabled status");
                }
            }
        }
    }
}
