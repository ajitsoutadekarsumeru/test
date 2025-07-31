using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApplicationUserDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ApplicationUserDetailsMapperConfiguration() : base()
        {
            CreateMap<ApplicationUserDetailsDto, ApplicationUserDetails>();
            CreateMap<ApplicationUserDetails, ApplicationUserDetailsDto>();
            CreateMap<ApplicationUserDetailsDtoWithId, ApplicationUserDetails>();
            CreateMap<ApplicationUserDetails, ApplicationUserDetailsDtoWithId>();
        }
    }
}