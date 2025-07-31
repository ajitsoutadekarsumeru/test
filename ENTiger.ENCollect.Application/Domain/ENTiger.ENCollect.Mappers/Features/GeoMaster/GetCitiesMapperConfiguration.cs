using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCitiesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCitiesMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, GetCitiesDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.CITY));
        }
    }
}