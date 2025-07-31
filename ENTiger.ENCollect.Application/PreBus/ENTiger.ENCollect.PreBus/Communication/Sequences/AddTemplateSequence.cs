using Sumeru.Flex;
using ENTiger.ENCollect.CommunicationModule.AddTemplateCommunicationPlugins;

namespace ENTiger.ENCollect.CommunicationModule
{
    public class AddTemplateSequence : FlexiBusinessRuleSequenceBase<AddTemplateDataPacket>
    {
        public AddTemplateSequence()
        {            
            this.Add<CheckForDuplicateTemplate>();
        }
    }
}
