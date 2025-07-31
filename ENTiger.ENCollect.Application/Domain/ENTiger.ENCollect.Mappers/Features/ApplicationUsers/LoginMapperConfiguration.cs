using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoginMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LoginMapperConfiguration() : base()
        {
            CreateMap<LoginDto, ApplicationUser>();
        }
    }
}