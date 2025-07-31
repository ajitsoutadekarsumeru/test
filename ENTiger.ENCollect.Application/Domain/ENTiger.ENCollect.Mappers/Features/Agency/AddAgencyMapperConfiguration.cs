using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddAgencyMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddAgencyMapperConfiguration() : base()
        {
            CreateMap<AddAgencyDto, Agency>()
                .ForMember(vm => vm.AgencyIdentifications, dm => dm.MapFrom(dModel => dModel.ProfileIdentification))
                .ForMember(vm => vm.CustomId, dm => dm.MapFrom(dModel => dModel.AgencyCode))
                .ForMember(vm => vm.AgencyTypeId, dm => dm.MapFrom(dModel => dModel.CollectionAgencyTypeId))
                .ForMember(vm => vm.AgencyWorkflowState, dm => dm.Ignore());

            CreateMap<AgencyIdentificationDto, AgencyIdentification>();
            CreateMap<AgencyIdentificationDocDto, AgencyIdentificationDoc>();
        }
    }
}