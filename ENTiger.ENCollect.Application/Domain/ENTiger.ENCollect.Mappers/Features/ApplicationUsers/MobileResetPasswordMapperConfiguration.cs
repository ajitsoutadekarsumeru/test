using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MobileResetPasswordMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MobileResetPasswordMapperConfiguration() : base()
        {
            CreateMap<MobileResetPasswordDto, ApplicationUser>();
        }
    }
}