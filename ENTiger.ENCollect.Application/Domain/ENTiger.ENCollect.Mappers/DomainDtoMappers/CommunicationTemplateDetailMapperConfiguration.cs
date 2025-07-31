using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CommunicationTemplateDetailMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CommunicationTemplateDetailMapperConfiguration() : base()
        {
            CreateMap<CommunicationTemplateDetailDto, CommunicationTemplateDetail>();
            CreateMap<CommunicationTemplateDetail, CommunicationTemplateDetailDto>();
            CreateMap<CommunicationTemplateDetailDtoWithId, CommunicationTemplateDetail>();
            CreateMap<CommunicationTemplateDetail, CommunicationTemplateDetailDtoWithId>();
        }
    }
}