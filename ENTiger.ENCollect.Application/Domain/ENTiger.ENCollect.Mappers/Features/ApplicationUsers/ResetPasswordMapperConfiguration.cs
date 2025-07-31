using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ResetPasswordMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ResetPasswordMapperConfiguration() : base()
        {
            CreateMap<ResetPasswordDto, ApplicationUser>();
        }
    }
}