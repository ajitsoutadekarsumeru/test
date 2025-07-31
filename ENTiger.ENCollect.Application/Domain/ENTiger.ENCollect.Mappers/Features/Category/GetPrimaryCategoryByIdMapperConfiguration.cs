using Sumeru.Flex;

namespace ENTiger.ENCollect.CategoryModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPrimaryCategoryByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetPrimaryCategoryByIdMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, GetPrimaryCategoryByIdDto>()
                .ForMember(o => o.Id, opt => opt.MapFrom(o => o.Id));
        }
    }
}