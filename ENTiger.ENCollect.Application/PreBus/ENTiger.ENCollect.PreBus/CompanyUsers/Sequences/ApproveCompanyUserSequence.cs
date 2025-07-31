using Sumeru.Flex;
using ENTiger.ENCollect.CompanyUsersModule.ApproveCompanyUserCompanyUsersPlugins;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class ApproveCompanyUserSequence : FlexiBusinessRuleSequenceBase<ApproveCompanyUserDataPacket>
    {
        public ApproveCompanyUserSequence()
        {
            this.Add<CheckCompanyUserLimitOnApproval>(); this.Add<CheckCreatedBy>(); 
        }
    }
}
