using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetRegionsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetRegionsMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, GetRegionsDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.Region));
        }
    }
}