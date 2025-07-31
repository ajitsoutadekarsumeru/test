using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgentsByRegionMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAgentsByRegionMapperConfiguration() : base()
        {
            CreateMap<AgencyUser, GetAgentsByRegionDto>()
                .ForMember(o => o.AgentCode, opt => opt.MapFrom(o => o.CustomId));
        }
    }
}