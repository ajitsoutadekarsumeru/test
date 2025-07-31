using Sumeru.Flex;

namespace ENTiger.ENCollect.CategoryModule
{
    public partial class GetSecondaryCategoryByParentIdMapperConfiguration : FlexMapperProfile
    {
        public GetSecondaryCategoryByParentIdMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, GetSecondaryCategoryByParentIdDto>()
                .ForMember(o => o.ParentName, opt => opt.MapFrom(o => o.Parent.Name));
        }
    }
}