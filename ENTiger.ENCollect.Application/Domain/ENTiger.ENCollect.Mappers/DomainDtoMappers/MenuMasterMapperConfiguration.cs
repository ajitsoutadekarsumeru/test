using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MenuMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public MenuMasterMapperConfiguration() : base()
        {
            CreateMap<MenuMasterDto, MenuMaster>();
            CreateMap<MenuMaster, MenuMasterDto>();
            CreateMap<MenuMasterDtoWithId, MenuMaster>();
            CreateMap<MenuMaster, MenuMasterDtoWithId>();
        }
    }
}