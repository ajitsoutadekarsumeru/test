using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateCompanyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateCompanyUserMapperConfiguration() : base()
        {
            CreateMap<UpdateCompanyUserDto, CompanyUser>()
                .ForMember(o => o.Designation, opt => opt.MapFrom(o => o.Roles));

            CreateMap<UserDesignationDto, CompanyUserDesignation>();

            // CreateMap<CompanyUserScopeOfWorkDto, CompanyUserScopeOfWork>()
            // .ForMember(o => o.SupervisingManagerId, opt => opt.MapFrom(o => o.ManagerId));

            // CreateMap<CompanyUserARMScopeOfWorkDto, CompanyUserARMScopeOfWork>()
            //.ForMember(o => o.SupervisingManagerId, opt => opt.MapFrom(o => o.ManagerId))
            //.ForMember(o => o.ReportingAgencyId, opt => opt.MapFrom(o => o.ReportingAgencyId));

            CreateMap<UserProductScopeDto, UserProductScope>();
            CreateMap<UserGeoScopeDto, UserGeoScope>();
            CreateMap<UserBucketScopeDto, UserBucketScope>();
        }
    }
}