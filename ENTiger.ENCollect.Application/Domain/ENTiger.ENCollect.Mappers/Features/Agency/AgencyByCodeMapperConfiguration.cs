using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyByCodeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyByCodeMapperConfiguration() : base()
        {
            CreateMap<Agency, AgencyByCodeDto>()
                 .ForMember(o => o.AgencyName, opt => opt.MapFrom(o => o.FirstName))
                 .ForMember(o => o.AgencyCode, opt => opt.MapFrom(o => o.CustomId));
        }
    }
}