using Sumeru.Flex;
using ENTiger.ENCollect.AgencyUsersModule.AddAgentAgencyUsersPlugins;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class AddAgentSequence : FlexiBusinessRuleSequenceBase<AddAgentDataPacket>
    {
        public AddAgentSequence()
        {
            this.Add<CheckDuplicateAgent>();this.Add<CheckDuplicateEmail>(); this.Add<CheckDuplicateMobile>(); this.Add<CheckPinCode>(); this.Add<ValidateFile>(); this.Add<CheckWalletLimit>();
        }
    }
}
