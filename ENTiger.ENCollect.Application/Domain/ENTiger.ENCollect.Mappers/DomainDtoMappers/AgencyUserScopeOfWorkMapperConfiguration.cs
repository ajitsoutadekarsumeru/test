using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserScopeOfWorkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyUserScopeOfWorkMapperConfiguration() : base()
        {
            CreateMap<AgencyUserScopeOfWorkDto, AgencyUserScopeOfWork>();
            CreateMap<AgencyUserScopeOfWork, AgencyUserScopeOfWorkDto>();
            CreateMap<AgencyUserScopeOfWorkDtoWithId, AgencyUserScopeOfWork>();
            CreateMap<AgencyUserScopeOfWork, AgencyUserScopeOfWorkDtoWithId>();
        }
    }
}