using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyPlaceOfWorkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyPlaceOfWorkMapperConfiguration() : base()
        {
            CreateMap<AgencyPlaceOfWorkDto, AgencyPlaceOfWork>();
            CreateMap<AgencyPlaceOfWork, AgencyPlaceOfWorkDto>();
            CreateMap<AgencyPlaceOfWorkDtoWithId, AgencyPlaceOfWork>();
            CreateMap<AgencyPlaceOfWork, AgencyPlaceOfWorkDtoWithId>();
        }
    }
}