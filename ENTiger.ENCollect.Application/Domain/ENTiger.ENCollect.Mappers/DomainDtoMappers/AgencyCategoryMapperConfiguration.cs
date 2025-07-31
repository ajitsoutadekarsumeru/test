using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyCategoryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyCategoryMapperConfiguration() : base()
        {
            CreateMap<AgencyCategoryDto, AgencyCategory>();
            CreateMap<AgencyCategory, AgencyCategoryDto>();
            CreateMap<AgencyCategoryDtoWithId, AgencyCategory>();
            CreateMap<AgencyCategory, AgencyCategoryDtoWithId>();
        }
    }
}