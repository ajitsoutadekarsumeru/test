using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserARMScopeOfWorkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyUserARMScopeOfWorkMapperConfiguration() : base()
        {
            CreateMap<CompanyUserARMScopeOfWorkDto, CompanyUserARMScopeOfWork>();
            CreateMap<CompanyUserARMScopeOfWork, CompanyUserARMScopeOfWorkDto>()
                .ForMember(o => o.ReportingAgencyFirstName, opt => opt.MapFrom(o => o.ReportingAgency.FirstName))
                .ForMember(o => o.ReportingAgencyLastName, opt => opt.MapFrom(o => o.ReportingAgency.LastName))
                .ForMember(o => o.ManagerId, opt => opt.MapFrom(o => o.SupervisingManager.Id))
                .ForMember(o => o.ManagerFirstName, opt => opt.MapFrom(o => o.SupervisingManager.FirstName))
                .ForMember(o => o.ManagerLastName, opt => opt.MapFrom(o => o.SupervisingManager.FirstName));

            CreateMap<CompanyUserARMScopeOfWorkDtoWithId, CompanyUserARMScopeOfWork>();
            CreateMap<CompanyUserARMScopeOfWork, CompanyUserARMScopeOfWorkDtoWithId>();
        }
    }
}