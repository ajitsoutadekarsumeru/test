using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
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
            CreateMap<GeoMaster, GetAreasDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.Area));
        }
    }
}