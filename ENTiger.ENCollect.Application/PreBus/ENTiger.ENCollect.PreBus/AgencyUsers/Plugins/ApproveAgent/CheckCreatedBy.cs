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

namespace ENTiger.ENCollect.AgencyUsersModule.ApproveAgentAgencyUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckCreatedBy : FlexiBusinessRuleBase, IFlexiBusinessRule<ApproveAgentDataPacket>
    {
        public override string Id { get; set; } = "3a12e79d05084cc8e16392c61adc1cdc";
        public override string FriendlyName { get; set; } = "CheckCreatedBy";

        protected readonly ILogger<CheckCreatedBy> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        public CheckCreatedBy(ILogger<CheckCreatedBy> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(ApproveAgentDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _flexAppContext = packet.Dto.GetAppContext();
            _repoFactory.Init(packet.Dto);

            string loggedInUserId = _flexAppContext.UserId;
            List<AgencyUser> users;
            List<string> ids = packet.Dto.AgentIds;

            users = await _repoFactory.GetRepo().FindAll<AgencyUser>().ByAgencyUserIds(ids).ToListAsync();

            foreach (var user in users)
            {
                if ((user.CreatedBy == loggedInUserId && user.CreatedDate.DateTime.Subtract(user.LastModifiedDate.DateTime).Seconds == 0) ||
                        (user.LastModifiedBy == loggedInUserId && user.CreatedDate.DateTime.Subtract(user.LastModifiedDate.DateTime).Seconds != 0))
                {
                    packet.AddError("Error", "You do not have permission to approve/reject");
                }
                else
                {
                    AgencyUser agenyUser = await FetchCollectionAgencyUserAsync(user);
                    if (agenyUser != null)
                    {
                        packet.AddError("Error", "Agent already exist for agency " + agenyUser.Agency.FirstName + " " + agenyUser.Agency.LastName);
                    }
                }
            }
        }

        private async Task<AgencyUser> FetchCollectionAgencyUserAsync(ApplicationUser appUser)
        {
            AgencyUserWorkflowState state = WorkflowStateFactory.GetCollectionAgencyUserWFState("disabled");
            return await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                .Where(x => x.PrimaryEMail == appUser.PrimaryEMail && x.PrimaryMobileNumber == appUser.PrimaryMobileNumber
                                        && x.AgencyUserWorkflowState.Name != state.Name && x.UserId != null
                                        && x.IsDeleted == false && x.Id != appUser.Id)
                                .IncludeAgencyUserWorkflow()
                                .IncludeAgencyUserAgency()
                                .FirstOrDefaultAsync();
        }
    }
}
