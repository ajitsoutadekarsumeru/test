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
    public partial class CheckDuplicateEmail : FlexiBusinessRuleBase, IFlexiBusinessRule<AddAgencyDataPacket>
    {
        public override string Id { get; set; } = "3a12d59f61ef280d6f77c0e40f251d10";
        public override string FriendlyName { get; set; } = "CheckDuplicateEmail";

        protected readonly ILogger<CheckDuplicateEmail> _logger;
        protected readonly IRepoFactory _repoFactory;

        public CheckDuplicateEmail(ILogger<CheckDuplicateEmail> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddAgencyDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            var PartyExists = await _repoFactory.GetRepo().FindAll<ApplicationOrg>().Where(p => p.PrimaryEMail == packet.Dto.PrimaryEMail).FirstOrDefaultAsync();

            if (PartyExists != null)
            {
                packet.AddError("Error", "Email Id already exists");
            }
        }
    }
}
