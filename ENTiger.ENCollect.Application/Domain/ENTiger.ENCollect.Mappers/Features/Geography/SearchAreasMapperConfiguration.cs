using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAreasMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAreasMapperConfiguration() : base()
        {
            CreateMap<Area, SearchAreaDto>()
                .ForMember(x => x.CityName, x => x.MapFrom(o => o.City.Name))
                .ForMember(x => x.CreatedDate, x => x.MapFrom(o => o.CreatedDate.DateTime))
                .ForMember(x => x.LastModifiedDate, x => x.MapFrom(o => o.LastModifiedDate.DateTime));
        }
    }
}