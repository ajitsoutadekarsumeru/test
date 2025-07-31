using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgentsListMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAgentsListMapperConfiguration() : base()
        {
            CreateMap<AgencyUser, GetAgentsListDto>()
                .ForMember(o => o.Code, opt => opt.MapFrom(o => o.CustomId));
        }
    }
}