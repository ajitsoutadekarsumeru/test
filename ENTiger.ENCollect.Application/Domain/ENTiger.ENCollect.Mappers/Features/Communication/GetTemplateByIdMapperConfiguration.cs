using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTemplateByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTemplateByIdMapperConfiguration() : base()
        {
            CreateMap<CommunicationTemplate, GetTemplateByIdDto>();

            CreateMap<CommunicationTemplateDetail, GetCommunicationTemplateDetailsDto>();
        }
    }
}