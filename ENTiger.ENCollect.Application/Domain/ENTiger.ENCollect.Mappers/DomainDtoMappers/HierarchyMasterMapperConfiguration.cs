using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HierarchyMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public HierarchyMasterMapperConfiguration() : base()
        {
            CreateMap<HierarchyMasterDto, HierarchyMaster>();
            CreateMap<HierarchyMaster, HierarchyMasterDto>();
            CreateMap<HierarchyMasterDtoWithId, HierarchyMaster>();
            CreateMap<HierarchyMaster, HierarchyMasterDtoWithId>();

        }
    }
}
