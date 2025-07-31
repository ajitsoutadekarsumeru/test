using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyIdentificationDocMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyIdentificationDocMapperConfiguration() : base()
        {
            CreateMap<AgencyIdentificationDocDto, AgencyIdentificationDoc>();
            CreateMap<AgencyIdentificationDoc, AgencyIdentificationDocDto>();
            CreateMap<AgencyIdentificationDocDtoWithId, AgencyIdentificationDoc>();
            CreateMap<AgencyIdentificationDoc, AgencyIdentificationDocDtoWithId>();
        }
    }
}