using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetGendersMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetGendersMapperConfiguration() : base()
        {
            CreateMap<CategoryItem, GetGendersDto>();
        }
    }
}