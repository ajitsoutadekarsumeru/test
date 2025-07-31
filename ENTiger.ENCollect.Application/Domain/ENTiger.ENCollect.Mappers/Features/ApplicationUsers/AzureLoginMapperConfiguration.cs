using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AzureLoginMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AzureLoginMapperConfiguration() : base()
        {
            CreateMap<AzureLoginDto, ApplicationUser>();
        }
    }
}