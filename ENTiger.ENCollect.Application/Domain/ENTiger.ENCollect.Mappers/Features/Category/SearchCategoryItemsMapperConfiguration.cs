using Sumeru.Flex;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class SearchCategoryItemsMapperConfiguration : FlexMapperProfile
    {
        public SearchCategoryItemsMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, SearchCategoryItemsDto>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Name));
        }
    }
}