using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoggedInUserDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LoggedInUserDetailsMapperConfiguration() : base()
        {
            CreateMap<ApplicationUser, LoggedInUserDetailsDto>();
        }
    }
}