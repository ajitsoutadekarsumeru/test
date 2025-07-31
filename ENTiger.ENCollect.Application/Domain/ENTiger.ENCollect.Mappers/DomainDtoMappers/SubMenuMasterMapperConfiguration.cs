using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SubMenuMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SubMenuMasterMapperConfiguration() : base()
        {
            CreateMap<SubMenuMasterDto, SubMenuMaster>();
            CreateMap<SubMenuMaster, SubMenuMasterDto>();
            CreateMap<SubMenuMasterDtoWithId, SubMenuMaster>();
            CreateMap<SubMenuMaster, SubMenuMasterDtoWithId>();
        }
    }
}