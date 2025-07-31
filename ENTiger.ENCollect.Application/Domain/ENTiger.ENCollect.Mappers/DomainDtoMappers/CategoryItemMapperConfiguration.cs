using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CategoryItemMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CategoryItemMapperConfiguration() : base()
        {
            CreateMap<CategoryItemDto, CategoryItem>();
            CreateMap<CategoryItemDto, CategoryItem>();
            CreateMap<CategoryItem, CategoryItemDto>();
            CreateMap<CategoryItemDtoWithId, CategoryItem>();
            CreateMap<CategoryItem, CategoryItemDtoWithId>();
        }
    }
}