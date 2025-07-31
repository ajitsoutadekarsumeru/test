using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyLoginOTPMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public VerifyLoginOTPMapperConfiguration() : base()
        {
            CreateMap<VerifyLoginOTPDto, ApplicationUser>();
        }
    }
}