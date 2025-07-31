using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddTemplateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddTemplateMapperConfiguration() : base()
        {
            CreateMap<AddTemplateDto, CommunicationTemplate>();
        }
    }
}