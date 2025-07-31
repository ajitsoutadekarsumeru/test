using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApplicationUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ApplicationUserMapperConfiguration() : base()
        {
            CreateMap<ApplicationUserDto, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDtoWithId, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserDtoWithId>();
        }
    }
}