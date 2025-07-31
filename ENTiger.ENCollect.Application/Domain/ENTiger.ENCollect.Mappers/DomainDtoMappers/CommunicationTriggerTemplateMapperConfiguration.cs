using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommunicationTriggerTemplateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CommunicationTriggerTemplateMapperConfiguration() : base()
        {
            CreateMap<CommunicationTriggerTemplateDto, TriggerDeliverySpec>();
            CreateMap<TriggerDeliverySpec, CommunicationTriggerTemplateDto>();
            CreateMap<CommunicationTriggerTemplateDtoWithId, TriggerDeliverySpec>();
            CreateMap<TriggerDeliverySpec, CommunicationTriggerTemplateDtoWithId>();

        }
    }
}
