using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetTemplatesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetTemplatesMapperConfiguration() : base()
        {
            CreateMap<CommunicationTemplate, GetTemplatesDto>()
                    .ForMember(s => s.TemplateName, d => d.MapFrom(dModel => dModel.Name));
        }
    }
}
