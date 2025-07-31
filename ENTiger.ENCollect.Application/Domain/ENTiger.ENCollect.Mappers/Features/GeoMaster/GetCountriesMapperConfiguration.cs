using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCountriesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetCountriesMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, GetCountriesDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.Country));
        }
    }
}