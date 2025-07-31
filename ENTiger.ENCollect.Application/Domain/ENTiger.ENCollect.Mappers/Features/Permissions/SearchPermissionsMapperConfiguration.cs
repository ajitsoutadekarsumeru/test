using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SearchPermissionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public SearchPermissionsMapperConfiguration() : base()
        {
            CreateMap<Permissions, SearchPermissionsDto>();

        }
    }
}
