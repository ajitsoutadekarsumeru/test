using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgenciesByNameMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAgenciesByNameMapperConfiguration() : base()
        {
            CreateMap<Agency, GetAgenciesByNameDto>()
                .ForMember(o => o.AgencyCode, opt => opt.MapFrom(o => o.CustomId));
        }
    }
}