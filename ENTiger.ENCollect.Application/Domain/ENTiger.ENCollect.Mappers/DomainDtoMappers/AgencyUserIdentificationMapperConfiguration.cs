using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserIdentificationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyUserIdentificationMapperConfiguration() : base()
        {
            CreateMap<AgencyUserIdentificationDto, AgencyUserIdentification>();
            CreateMap<AgencyUserIdentification, AgencyUserIdentificationDto>();
            CreateMap<AgencyUserIdentificationDtoWithId, AgencyUserIdentification>();
            CreateMap<AgencyUserIdentification, AgencyUserIdentificationDtoWithId>();
        }
    }
}