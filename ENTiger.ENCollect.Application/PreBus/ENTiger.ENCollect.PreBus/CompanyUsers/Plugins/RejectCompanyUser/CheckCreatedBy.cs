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

namespace ENTiger.ENCollect.CompanyUsersModule.RejectCompanyUserCompanyUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CheckCreatedBy : FlexiBusinessRuleBase, IFlexiBusinessRule<RejectCompanyUserDataPacket>
    {
        public override string Id { get; set; } = "3a12da5755ca9b7fd5e22ff31132f86b";
        public override string FriendlyName { get; set; } = "CheckCreatedBy";

        protected readonly ILogger<CheckCreatedBy> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;

        public CheckCreatedBy(ILogger<CheckCreatedBy> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(RejectCompanyUserDataPacket packet)
        {
            _flexAppContext = packet.Dto.GetAppContext();  //do not remove this line

            string loggedInUserId = _flexAppContext.UserId;
            List<ApplicationUser> users;
            List<string> ids = packet.Dto.companyUserIds;

            //Uncomment the below line if validating against a db data using your repo
            _repoFactory.Init(packet.Dto);

            //If any validation fails, uncomment and use the below line of code to add error to the packet
            users = await _repoFactory.GetRepo().FindAll<ApplicationUser>().ByTFlexIds(ids).ToListAsync();

            foreach (var user in users)
            {
                if (user.CreatedBy == loggedInUserId )
                {
                    packet.AddError("Error", "You do not have permission to reject.");
                }
            }
        }
    }
}
