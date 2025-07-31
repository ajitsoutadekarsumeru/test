using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyTypeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyTypeMapperConfiguration() : base()
        {
            CreateMap<AgencyTypeDto, AgencyType>();
            CreateMap<AgencyType, AgencyTypeDto>();
            CreateMap<AgencyTypeDtoWithId, AgencyType>();
            CreateMap<AgencyType, AgencyTypeDtoWithId>();
        }
    }
}