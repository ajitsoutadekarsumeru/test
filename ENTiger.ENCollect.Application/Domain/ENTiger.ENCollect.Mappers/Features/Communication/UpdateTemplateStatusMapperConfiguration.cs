using Sumeru.Flex;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateTemplateStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateTemplateStatusMapperConfiguration() : base()
        {
            CreateMap<UpdateTemplateStatusDto, CommunicationTemplate>();
        }
    }
}