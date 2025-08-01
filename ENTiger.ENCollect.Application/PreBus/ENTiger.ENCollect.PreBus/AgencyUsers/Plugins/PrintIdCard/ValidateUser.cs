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

namespace ENTiger.ENCollect.AgencyUsersModule.PrintIdCardAgencyUsersPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateUser : FlexiBusinessRuleBase, IFlexiBusinessRule<PrintIdCardDataPacket>
    {
        public override string Id { get; set; } = "3a13ba428be1f70124cba9effa60fe4e";
        public override string FriendlyName { get; set; } = "ValidateUser";

        protected readonly ILogger<ValidateUser> _logger;
        protected readonly IRepoFactory _repoFactory;
        protected FlexAppContextBridge? _flexAppContext;
        protected readonly ICustomUtility _customUtility;
        public ValidateUser(ILogger<ValidateUser> logger, IRepoFactory repoFactory, ICustomUtility customUtility)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
        }

        public virtual async Task Validate(PrintIdCardDataPacket packet)
        {
            //Uncomment the below line if validating against a db data using your repo
            _flexAppContext = packet.Dto.GetAppContext();  //do not remove this line
            _repoFactory.Init(packet.Dto);

            AgencyUser agencyUser = await _repoFactory.GetRepo().FindAll<AgencyUser>().ByAgencyUserId(packet.Dto.Id).FirstOrDefaultAsync();
            if (agencyUser == null)
            {
                packet.AddError("Error", "Invaild Request");
            }

            if (!packet.HasError)
            {
                packet.IdCardNumber = await _customUtility.GetNextCustomIdAsync(_flexAppContext, CustomIdEnum.IDCardNumber.Value);
            }
        }
    }
}
