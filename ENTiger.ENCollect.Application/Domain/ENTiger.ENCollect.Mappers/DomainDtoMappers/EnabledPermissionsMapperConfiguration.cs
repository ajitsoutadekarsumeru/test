using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EnabledPermissionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public EnabledPermissionsMapperConfiguration() : base()
        {
            CreateMap<EnabledPermissionsDto, EnabledPermission>();
            CreateMap<EnabledPermission, EnabledPermissionsDto>();
            CreateMap<EnabledPermissionsDtoWithId, EnabledPermission>();
            CreateMap<EnabledPermission, EnabledPermissionsDtoWithId>();

        }
    }
}
