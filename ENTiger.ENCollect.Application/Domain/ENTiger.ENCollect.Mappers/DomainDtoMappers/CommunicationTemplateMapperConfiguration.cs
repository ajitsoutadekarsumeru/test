using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CommunicationTemplateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CommunicationTemplateMapperConfiguration() : base()
        {
            CreateMap<CommunicationTemplateDto, CommunicationTemplate>();
            CreateMap<CommunicationTemplate, CommunicationTemplateDto>();
            CreateMap<CommunicationTemplateDtoWithId, CommunicationTemplate>();
            CreateMap<CommunicationTemplate, CommunicationTemplateDtoWithId>();
        }
    }
}