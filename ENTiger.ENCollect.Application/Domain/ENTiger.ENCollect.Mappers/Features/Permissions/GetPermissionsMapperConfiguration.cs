using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPermissionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetPermissionsMapperConfiguration() : base()
        {
            CreateMap<Permissions, GetPermissionsDto>();

        }
    }
}
