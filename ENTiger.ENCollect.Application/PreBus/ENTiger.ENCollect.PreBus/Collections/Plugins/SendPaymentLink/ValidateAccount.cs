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

namespace ENTiger.ENCollect.CollectionsModule.SendPaymentLinkCollectionsPlugins
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ValidateAccount : FlexiBusinessRuleBase, IFlexiBusinessRule<SendPaymentLinkDataPacket>
    {
        public override string Id { get; set; } = "3a138ee2be529f2c8ac6808032e13883";
        public override string FriendlyName { get; set; } = "ValidateAccount";

        protected readonly ILogger<ValidateAccount> _logger;
        protected readonly IRepoFactory _repoFactory;

        public ValidateAccount(ILogger<ValidateAccount> logger, IRepoFactory repoFactory)
        {
            _logger = logger;
            _repoFactory = repoFactory;
        }

        public virtual async Task Validate(SendPaymentLinkDataPacket packet)
        {
            _repoFactory.Init(packet.Dto);

            if (string.IsNullOrEmpty(packet.Dto.MobileNo) && string.IsNullOrEmpty(packet.Dto.EMailId))
            {
                packet.AddError("Error", "Please Enter Mobile Number or EmailId");
            }

            var account = await _repoFactory.GetRepo().FindAll<LoanAccount>()
                                    .Where(i => i.AGREEMENTID == packet.Dto.Accountno)
                                    .FirstOrDefaultAsync();

            if (account == null)
            {
                packet.AddError("Error", "Invalid Account - " + packet.Dto.Accountno);
            }
        }
    }
}
