using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgenciesByTypeIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAgenciesByTypeIdMapperConfiguration() : base()
        {
            CreateMap<Agency, GetAgenciesByTypeIdDto>()
                .ForMember(o => o.AgencyId, opt => opt.MapFrom(o => o.Id))
                .ForMember(o => o.AgencyCode, opt => opt.MapFrom(o => o.CustomId))
                .ForMember(o => o.AgencyName, opt => opt.MapFrom(o => $"{o.FirstName} {o.LastName}"));
        }
    }
}