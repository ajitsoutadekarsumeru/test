using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetUsersByIdsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetUsersByIdsMapperConfiguration() : base()
        {
            CreateMap<ApplicationUser, GetUsersByIdsDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.CustomId));

        }
    }
}
