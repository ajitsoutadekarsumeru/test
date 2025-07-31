using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyMapperConfiguration() : base()
        {
            CreateMap<CompanyDto, Company>();
            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDtoWithId, Company>();
            CreateMap<Company, CompanyDtoWithId>();
        }
    }
}