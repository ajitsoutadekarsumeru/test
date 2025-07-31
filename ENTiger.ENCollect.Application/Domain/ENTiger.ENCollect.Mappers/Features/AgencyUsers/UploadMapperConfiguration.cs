using ENTiger.ENCollect.CommonModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UploadMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UploadMapperConfiguration() : base()
        {
            CreateMap<UploadDto, AgencyUserIdentificationDoc>();
            CreateMap<FileDto, AgencyUserIdentificationDoc>();
        }
    }
}