using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTCAgenciesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTCAgenciesMapperConfiguration() : base()
        {
            CreateMap<Agency, GetTCAgenciesDto>()
            .ForMember(dest => dest.AgencyCode, opt => opt.MapFrom(src => src.CustomId))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            ;
        }
    }
}