using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MobileLoginMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MobileLoginMapperConfiguration() : base()
        {
            CreateMap<MobileLoginDto, ApplicationUser>();
        }
    }
}