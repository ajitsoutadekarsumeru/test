using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserDesignationMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyUserDesignationMapperConfiguration() : base()
        {
            CreateMap<CompanyUserDesignationDto, CompanyUserDesignation>();
            CreateMap<CompanyUserDesignation, CompanyUserDesignationDto>();
            CreateMap<CompanyUserDesignationDtoWithId, CompanyUserDesignation>();
            CreateMap<CompanyUserDesignation, CompanyUserDesignationDtoWithId>();
        }
    }
}