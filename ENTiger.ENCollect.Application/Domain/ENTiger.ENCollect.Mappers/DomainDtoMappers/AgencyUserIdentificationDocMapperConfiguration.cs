using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserIdentificationDocMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyUserIdentificationDocMapperConfiguration() : base()
        {
            CreateMap<AgencyUserIdentificationDocDto, AgencyUserIdentificationDoc>();
            CreateMap<AgencyUserIdentificationDoc, AgencyUserIdentificationDocDto>();
            CreateMap<AgencyUserIdentificationDocDtoWithId, AgencyUserIdentificationDoc>();
            CreateMap<AgencyUserIdentificationDoc, AgencyUserIdentificationDocDtoWithId>();
        }
    }
}