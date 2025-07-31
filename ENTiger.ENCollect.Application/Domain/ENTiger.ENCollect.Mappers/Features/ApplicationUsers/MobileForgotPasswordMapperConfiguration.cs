using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MobileForgotPasswordMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MobileForgotPasswordMapperConfiguration() : base()
        {
            CreateMap<MobileForgotPasswordDto, ApplicationUser>();
        }
    }
}