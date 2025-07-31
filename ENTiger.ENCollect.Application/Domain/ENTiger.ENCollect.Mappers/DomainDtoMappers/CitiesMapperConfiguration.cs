using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CitiesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CitiesMapperConfiguration() : base()
        {
            CreateMap<CitiesDto, Cities>();
            CreateMap<Cities, CitiesDto>();
            CreateMap<CitiesDtoWithId, Cities>();
            CreateMap<Cities, CitiesDtoWithId>();
        }
    }
}