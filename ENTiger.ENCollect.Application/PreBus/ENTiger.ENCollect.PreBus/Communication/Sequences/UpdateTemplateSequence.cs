using Sumeru.Flex;
using ENTiger.ENCollect.CommunicationModule.UpdateTemplateCommunicationPlugins;

namespace ENTiger.ENCollect.CommunicationModule
{
    public class UpdateTemplateSequence : FlexiBusinessRuleSequenceBase<UpdateTemplateDataPacket>
    {
        public UpdateTemplateSequence()
        {            
            this.Add<CheckTemplateMapping>();
        }
    }
}
