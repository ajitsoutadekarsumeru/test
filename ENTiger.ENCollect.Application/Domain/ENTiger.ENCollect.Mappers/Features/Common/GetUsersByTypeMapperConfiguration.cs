using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersByTypeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetUsersByTypeMapperConfiguration() : base()
        {
            CreateMap<ApplicationUser, GetUsersByTypeDto>();
        }
    }
}