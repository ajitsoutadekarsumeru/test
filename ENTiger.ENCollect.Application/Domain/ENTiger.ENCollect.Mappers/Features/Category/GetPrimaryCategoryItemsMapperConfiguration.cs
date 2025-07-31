using Sumeru.Flex;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class GetPrimaryCategoryItemsMapperConfiguration : FlexMapperProfile
    {
        public GetPrimaryCategoryItemsMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, GetPrimaryCategoryItemsDto>()
                .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Name)); ;
        }
    }
}