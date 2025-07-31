using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermissionSchemesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionSchemesMapperConfiguration() : base()
        {
            CreateMap<PermissionSchemesDto, PermissionSchemes>();
            CreateMap<PermissionSchemes, PermissionSchemesDto>();
            CreateMap<PermissionSchemesDtoWithId, PermissionSchemes>();
            CreateMap<PermissionSchemes, PermissionSchemesDtoWithId>();

        }
    }
}
