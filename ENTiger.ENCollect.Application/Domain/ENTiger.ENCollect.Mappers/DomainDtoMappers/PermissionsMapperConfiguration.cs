using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermissionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionsMapperConfiguration() : base()
        {
            CreateMap<PermissionsDto, Permissions>();
            CreateMap<Permissions, PermissionsDto>();
            CreateMap<PermissionsDtoWithId, Permissions>();
            CreateMap<Permissions, PermissionsDtoWithId>();

        }
    }
}
