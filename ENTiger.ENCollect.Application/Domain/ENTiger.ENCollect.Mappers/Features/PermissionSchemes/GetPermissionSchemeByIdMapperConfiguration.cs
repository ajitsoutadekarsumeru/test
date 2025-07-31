using ENTiger.ENCollect.PermissionsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetPermissionSchemeByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetPermissionSchemeByIdMapperConfiguration() : base()
        {
            CreateMap<PermissionSchemes, GetPermissionSchemeByIdDto>()
             .ForMember(dest => dest.EnabledPermissions, opt => opt.MapFrom(src => src.EnabledPermissions));
            CreateMap<EnabledPermission, EnabledPermissionSchemeDto>()
                .ForMember(dest => dest.PermissionId, opt => opt.MapFrom(src => src.Permission.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Permission.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Permission.Description))
                .ForMember(dest => dest.Section, opt => opt.MapFrom(src => src.Permission.Section));

            CreateMap<PermissionSchemeChangeLog, GetPermissionSchemeChangeLogDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate.DateTime));
        }
    }
}
