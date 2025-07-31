using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchACMMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchACMMapperConfiguration() : base()
        {
            CreateMap<UserAccessRights, SearchACMDto>()
                .ForMember(dest => dest.Menu, opt => opt.MapFrom(src => src.MenuMaster.MenuName))
                .ForMember(dest => dest.SubMenu, opt => opt.MapFrom(src => src.SubMenuMaster.SubMenuName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ActionMaster.Id))
                .ForMember(dest => dest.Scope, opt => opt.MapFrom(src => src.ActionMaster.Name))
                .ForMember(dest => dest.HasAccess, opt => opt.MapFrom(src => src.ActionMaster.HasAccess));
        }
    }
}