using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgentsByAgencyIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAgentsByAgencyIdMapperConfiguration() : base()
        {
            CreateMap<AgencyUser, GetAgentsByAgencyIdDto>()
                .ForMember(o => o.Code, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.Name, opt => opt.MapFrom(o => o.FirstName));
        }
    }
}