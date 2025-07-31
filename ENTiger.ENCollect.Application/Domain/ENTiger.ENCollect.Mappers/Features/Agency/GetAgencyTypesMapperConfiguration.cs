using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgencyTypesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetAgencyTypesMapperConfiguration() : base()
        {
            CreateMap<AgencyType, GetAgencyTypesDto>()
                 .ForMember(o => o.AgencyType, opt => opt.MapFrom(o => o.MainType))
                 .ForMember(o => o.AgencySubType, opt => opt.MapFrom(o => o.SubType));
        }
    }
}