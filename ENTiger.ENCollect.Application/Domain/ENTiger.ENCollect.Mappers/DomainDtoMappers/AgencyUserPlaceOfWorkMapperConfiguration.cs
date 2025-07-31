using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserPlaceOfWorkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyUserPlaceOfWorkMapperConfiguration() : base()
        {
            CreateMap<AgencyUserPlaceOfWorkDto, AgencyUserPlaceOfWork>();
            CreateMap<AgencyUserPlaceOfWork, AgencyUserPlaceOfWorkDto>();
            CreateMap<AgencyUserPlaceOfWorkDtoWithId, AgencyUserPlaceOfWork>();
            CreateMap<AgencyUserPlaceOfWork, AgencyUserPlaceOfWorkDtoWithId>();
        }
    }
}