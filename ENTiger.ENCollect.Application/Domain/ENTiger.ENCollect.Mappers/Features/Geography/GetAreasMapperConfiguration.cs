using Sumeru.Flex;

namespace ENTiger.ENCollect.GeographyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAreasMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAreasMapperConfiguration() : base()
        {
            CreateMap<Area, GetAreaDto>()
                .ForMember(x => x.CityName, x => x.MapFrom(o => o.City.Name))
                .ForMember(x => x.CreatedDate, x => x.MapFrom(o => o.CreatedDate.DateTime))
                .ForMember(x => x.LastModifiedDate, x => x.MapFrom(o => o.LastModifiedDate.DateTime));
        }
    }
}