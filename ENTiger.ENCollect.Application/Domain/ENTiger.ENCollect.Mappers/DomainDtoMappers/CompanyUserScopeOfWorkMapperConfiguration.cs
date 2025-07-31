using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserScopeOfWorkMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CompanyUserScopeOfWorkMapperConfiguration() : base()
        {
            CreateMap<CompanyUserScopeOfWorkDto, CompanyUserScopeOfWork>();
            CreateMap<CompanyUserScopeOfWork, CompanyUserScopeOfWorkDto>()
            .ForMember(o => o.ManagerId, opt => opt.MapFrom(o => o.SupervisingManagerId))
            .ForMember(o => o.ManagerFirstName, opt => opt.MapFrom(o => o.SupervisingManager.FirstName))
            .ForMember(o => o.ManagerLastName, opt => opt.MapFrom(o => o.SupervisingManager.LastName));

            CreateMap<CompanyUserScopeOfWorkDtoWithId, CompanyUserScopeOfWork>();
            CreateMap<CompanyUserScopeOfWork, CompanyUserScopeOfWorkDtoWithId>();
        }
    }
}