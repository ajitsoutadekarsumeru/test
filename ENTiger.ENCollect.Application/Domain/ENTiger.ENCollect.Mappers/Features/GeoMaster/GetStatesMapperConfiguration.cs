using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoMasterModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetStatesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetStatesMapperConfiguration() : base()
        {
            CreateMap<GeoMaster, GetStatesDto>().ForMember(d => d.Name, s => s.MapFrom(s => s.State));
        }
    }
}