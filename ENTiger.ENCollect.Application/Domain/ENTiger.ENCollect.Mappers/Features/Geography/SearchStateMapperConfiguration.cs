using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchStateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchStateMapperConfiguration() : base()
        {
            CreateMap<State, SearchStateDto>();
        }
    }
}