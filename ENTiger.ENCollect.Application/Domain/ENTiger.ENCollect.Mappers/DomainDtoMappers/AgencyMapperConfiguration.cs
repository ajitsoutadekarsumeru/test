using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyMapperConfiguration() : base()
        {
            CreateMap<AgencyDto, Agency>();
            CreateMap<Agency, AgencyDto>();
            CreateMap<AgencyDtoWithId, Agency>();
            CreateMap<Agency, AgencyDtoWithId>();
        }
    }
}