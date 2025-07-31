using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyScopeOfWorkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyScopeOfWorkMapperConfiguration() : base()
        {
            CreateMap<AgencyScopeOfWorkDto, AgencyScopeOfWork>();
            CreateMap<AgencyScopeOfWork, AgencyScopeOfWorkDto>();
            CreateMap<AgencyScopeOfWorkDtoWithId, AgencyScopeOfWork>();
            CreateMap<AgencyScopeOfWork, AgencyScopeOfWorkDtoWithId>();
        }
    }
}