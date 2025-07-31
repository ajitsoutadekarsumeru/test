using Sumeru.Flex;
using ENTiger.ENCollect.AgencyUsersModule.ApproveAgentAgencyUsersPlugins;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class ApproveAgentSequence : FlexiBusinessRuleSequenceBase<ApproveAgentDataPacket>
    {
        public ApproveAgentSequence()
        {

            this.Add<CheckAgencyUserLimitOnApproval>(); this.Add<CheckCreatedBy>(); 
        }
    }
}
