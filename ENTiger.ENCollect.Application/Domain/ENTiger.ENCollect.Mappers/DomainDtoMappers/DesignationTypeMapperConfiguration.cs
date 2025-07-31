using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DesignationTypeMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public DesignationTypeMapperConfiguration() : base()
        {
            CreateMap<DesignationTypeDto, DesignationType>();
            CreateMap<DesignationType, DesignationTypeDto>();
            CreateMap<DesignationTypeDtoWithId, DesignationType>();
            CreateMap<DesignationType, DesignationTypeDtoWithId>();
        }
    }
}