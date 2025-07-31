using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ForgotPasswordMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ForgotPasswordMapperConfiguration() : base()
        {
            CreateMap<ForgotPasswordDto, ApplicationUser>();
        }
    }
}