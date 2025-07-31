using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAppVersionDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAppVersionDetailsMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, GetAppVersionDetailsDto>();
        }
    }
}