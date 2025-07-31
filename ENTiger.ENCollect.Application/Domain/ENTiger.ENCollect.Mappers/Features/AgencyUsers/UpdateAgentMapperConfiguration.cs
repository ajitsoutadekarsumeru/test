using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateAgentMapperConfiguration() : base()
        {
            CreateMap<UpdateAgentDto, AgencyUser>()
            .ForMember(o => o.Designation, opt => opt.MapFrom(o => o.Roles))
            .ForMember(vm => vm.AgencyUserIdentifications, dm => dm.MapFrom(dModel => dModel.profileIdentification));

            CreateMap<UserDesignationDto, AgencyUserDesignation>();

            CreateMap<AgencyUserIdentificationDto, AgencyUserIdentification>();
            CreateMap<AgencyUserIdentificationDocDto, AgencyUserIdentificationDoc>();
        }
    }
}