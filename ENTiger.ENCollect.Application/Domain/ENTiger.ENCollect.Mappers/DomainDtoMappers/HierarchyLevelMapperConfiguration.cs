using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class HierarchyLevelMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public HierarchyLevelMapperConfiguration() : base()
        {
            CreateMap<HierarchyLevelDto, HierarchyLevel>();
            CreateMap<HierarchyLevel, HierarchyLevelDto>();
            CreateMap<HierarchyLevelDtoWithId, HierarchyLevel>();
            CreateMap<HierarchyLevel, HierarchyLevelDtoWithId>();

        }
    }
}
