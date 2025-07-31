using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CategoryMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CategoryMasterMapperConfiguration() : base()
        {
            CreateMap<CategoryMasterDto, CategoryMaster>();
            CreateMap<CategoryMaster, CategoryMasterDto>();
            CreateMap<CategoryMasterDtoWithId, CategoryMaster>();
            CreateMap<CategoryMaster, CategoryMasterDtoWithId>();
        }
    }
}