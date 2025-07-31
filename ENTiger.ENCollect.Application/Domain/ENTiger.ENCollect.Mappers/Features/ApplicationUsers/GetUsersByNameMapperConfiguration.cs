using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersByNameMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetUsersByNameMapperConfiguration() : base()
        {
            CreateMap<ApplicationUser, GetUsersByNameDto>();
        }
    }
}