using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ADLoginMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public ADLoginMapperConfiguration() : base()
        {
            CreateMap<ADLoginDto, ApplicationUser>();
        }
    }
}