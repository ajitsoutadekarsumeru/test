using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddCompanyUserMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddCompanyUserMapperConfiguration() : base()
        {
            CreateMap<AddCompanyUserDto, CompanyUser>()
                .ForMember(o => o.Designation, opt => opt.MapFrom(o => o.Roles)); 

            CreateMap<UserDesignationDto, CompanyUserDesignation>();

            CreateMap<UserProductScopeDto, UserProductScope>();
            CreateMap<UserGeoScopeDto, UserGeoScope>();
            CreateMap<UserBucketScopeDto, UserBucketScope>();
        }
    }
}