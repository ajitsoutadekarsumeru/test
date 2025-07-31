using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyForgotPasswordOTPMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public VerifyForgotPasswordOTPMapperConfiguration() : base()
        {
            CreateMap<VerifyForgotPasswordOTPDto, ApplicationUser>();
        }
    }
}