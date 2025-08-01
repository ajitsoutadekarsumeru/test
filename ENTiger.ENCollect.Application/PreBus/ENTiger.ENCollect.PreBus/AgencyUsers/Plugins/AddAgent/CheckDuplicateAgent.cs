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

namespace ENTiger.ENCollect.AgencyUsersModule.AddAgentAgencyUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckDuplicateAgent : FlexiBusinessRuleBase, IFlexiBusinessRule<AddAgentDataPacket>
    {
        public override string Id { get; set; } = "3a12d148e19dee580b4013a479fdc376";
        public override string FriendlyName { get; set; } = "CheckDuplicateAgent";

        protected readonly ILogger<CheckDuplicateAgent> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public CheckDuplicateAgent(ILogger<CheckDuplicateAgent> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(AddAgentDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);


            if (!string.IsNullOrEmpty(packet.Dto.AgencyId))
            {
                AgencyUserWorkflowState state = WorkflowStateFactory.GetCollectionAgencyUserWFState("disabled");

                var PartyExists = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                                .IncludeAgencyUserWorkflow()
                                                .Where(p => p.PrimaryEMail == packet.Dto.PrimaryEMail && p.PrimaryMobileNumber == packet.Dto.PrimaryMobileNumber
                                                        && p.AgencyId == packet.Dto.AgencyId && p.AgencyUserWorkflowState.Name == state.Name)
                                                .FirstOrDefaultAsync();

                if (PartyExists != null)
                {
                    packet.AddError("Error", "Agent already exist in Agency "+PartyExists.FirstName);
                }
            }
            else if (string.IsNullOrEmpty(packet.Dto.overwriteAgentId))
            {
                var PartyExists = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                            .Where(p => p.FirstName == packet.Dto.FirstName && p.LastName == packet.Dto.LastName
                                                    && p.DateOfBirth == packet.Dto.DateOfBirth && p.UDIDNumber == packet.Dto.UDIDNumber)
                                            .ToListAsync();

                if (PartyExists != null)
                {
                    packet.AddError("Error", "Agent exists with same First Name,Last Lame, DOB and UDID");
                }
            }
            else
            {
                AgencyUserWorkflowState state = WorkflowStateFactory.GetCollectionAgencyUserWFState("disabled");

                var PartyExists = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                            .IncludeAgencyUserWorkflow()
                                            .Where(p => p.FirstName == packet.Dto.FirstName &&
                                                    p.LastName == packet.Dto.LastName &&
                                                    p.DateOfBirth == packet.Dto.DateOfBirth &&
                                                    p.UDIDNumber == packet.Dto.UDIDNumber &&
                                                    p.Id != packet.Dto.overwriteAgentId &&
                                                    p.AgencyUserWorkflowState.Name != state.Name &&
                                                    p.UserId != null && p.IsDeleted == false)
                                            .FirstOrDefaultAsync();
                if (PartyExists != null)
                {
                    packet.AddError("Error", "Agent exists with same First Name,Last Name, DOB and UDID");
                }
            }
        }
    }
}
