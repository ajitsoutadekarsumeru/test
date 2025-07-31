using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LogoutMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public LogoutMapperConfiguration() : base()
        {
            CreateMap<LogoutDto, AgencyUser>();
        }
    }
}