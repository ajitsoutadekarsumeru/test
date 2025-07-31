using ENTiger.ENCollect.CommonModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUploadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyUploadMapperConfiguration() : base()
        {
            CreateMap<AgencyUploadDto, AgencyIdentificationDoc>();
            CreateMap<FileDto, AgencyIdentificationDoc>();
        }
    }
}