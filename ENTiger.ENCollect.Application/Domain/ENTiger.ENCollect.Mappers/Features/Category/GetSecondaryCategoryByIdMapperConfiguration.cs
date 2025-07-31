using Sumeru.Flex;

namespace ENTiger.ENCollect.CategoryModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetSecondaryCategoryByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetSecondaryCategoryByIdMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, GetSecondaryCategoryByIdDto>()
                 .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Name));
        }
    }
}