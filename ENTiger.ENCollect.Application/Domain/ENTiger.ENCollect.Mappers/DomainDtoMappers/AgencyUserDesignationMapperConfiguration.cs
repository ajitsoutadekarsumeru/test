using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserDesignationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgencyUserDesignationMapperConfiguration() : base()
        {
            CreateMap<AgencyUserDesignationDto, AgencyUserDesignation>();
            CreateMap<AgencyUserDesignation, AgencyUserDesignationDto>();
            CreateMap<AgencyUserDesignationDtoWithId, AgencyUserDesignation>();
            CreateMap<AgencyUserDesignation, AgencyUserDesignationDtoWithId>();
        }
    }
}