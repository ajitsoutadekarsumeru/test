using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgenciesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAgenciesMapperConfiguration() : base()
        {
            CreateMap<Agency, GetAgenciesDto>()
                 .ForMember(o => o.AgencyCode, opt => opt.MapFrom(o => o.CustomId))
                   .ForMember(o => o.LastName, opt => opt.MapFrom(o => !string.IsNullOrEmpty(o.LastName) ? o.LastName : string.Empty));
        }
    }
}