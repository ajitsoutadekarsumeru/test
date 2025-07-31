using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddAgentMapperConfiguration() : base()
        {
            CreateMap<AddAgentDto, AgencyUser>()
            .ForMember(o => o.Designation, opt => opt.MapFrom(o => o.Roles))
            .ForMember(vm => vm.AgencyUserIdentifications, dm => dm.MapFrom(dModel => dModel.profileIdentification));

            CreateMap<UserDesignationDto, AgencyUserDesignation>();

            CreateMap<AgencyUserIdentificationDto, AgencyUserIdentification>();
            CreateMap<AgencyUserIdentificationDocDto, AgencyUserIdentificationDoc>();

            CreateMap<UserProductScopeDto, UserProductScope>();
            CreateMap<UserGeoScopeDto, UserGeoScope>();
            CreateMap<UserBucketScopeDto, UserBucketScope>();
        }
    }
}