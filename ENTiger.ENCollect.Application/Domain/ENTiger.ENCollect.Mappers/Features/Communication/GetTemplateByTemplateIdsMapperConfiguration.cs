using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTemplateByTemplateIdsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTemplateByTemplateIdsMapperConfiguration() : base()
        {
            CreateMap<CommunicationTemplate, GetTemplateDetailsByTemplateIdsDto>();
        }
    }
}
