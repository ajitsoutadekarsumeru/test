﻿// <auto-generated>
//     This code was generated by Sumeru FlexGen.
//     Template Version: TemplateVersion
//
//     Do not rename the file
//     TODO:
//     Implement your validations in the Validate(..) method.
// </auto-generated>

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.AgencyModule.AddAgencyAgencyPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckDuplicateAgency : FlexiBusinessRuleBase, IFlexiBusinessRule<AddAgencyDataPacket>
    {
        public override string Id { get; set; } = "3a12d59f589820733674e97a5741cc34";
        public override string FriendlyName { get; set; } = "CheckDuplicateAgency";

        protected readonly ILogger<CheckDuplicateAgency> _logger;
        protected readonly IRepoFactory _repoFactory;

        public CheckDuplicateAgency(ILogger<CheckDuplicateAgency> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddAgencyDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            var PartyExists = await _repoFactory.GetRepo().FindAll<Agency>().Where(p => p.CustomId == packet.Dto.AgencyCode).FirstOrDefaultAsync();

            if (PartyExists != null)
            {
                packet.AddError("Error", "Agency Code already exists");
            }
        }
    }
}
