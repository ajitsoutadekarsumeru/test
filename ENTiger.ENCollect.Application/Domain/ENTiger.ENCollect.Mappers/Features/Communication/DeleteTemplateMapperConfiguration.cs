using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeleteTemplateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DeleteTemplateMapperConfiguration() : base()
        {
            CreateMap<DeleteTemplateDto, CommunicationTemplate>();
        }
    }
}