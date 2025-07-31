using Sumeru.Flex;
using ENTiger.ENCollect.CommunicationModule.UpdateTemplateStatusCommunicationPlugins;

namespace ENTiger.ENCollect.CommunicationModule
{
    public class UpdateTemplateStatusSequence : FlexiBusinessRuleSequenceBase<UpdateTemplateStatusDataPacket>
    {
        public UpdateTemplateStatusSequence()
        {
            
            this.Add<ValidateTemplateStatus>(); 
        }
    }
}
