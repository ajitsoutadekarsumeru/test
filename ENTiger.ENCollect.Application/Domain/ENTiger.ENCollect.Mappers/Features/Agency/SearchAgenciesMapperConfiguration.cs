using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchAgenciesMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public SearchAgenciesMapperConfiguration() : base()
        {
            CreateMap<Agency, SearchAgenciesDto>()
                .ForMember(vm => vm.AgencyName, Dm => Dm.MapFrom(dModel => dModel.FirstName))
                .ForMember(vm => vm.AgencyCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                .ForMember(vm => vm.AgencyType, Dm => Dm.MapFrom(dModel => dModel.AgencyType.MainType))
                .ForMember(vm => vm.AgencyWFState, Dm => Dm.MapFrom(dModel => dModel.AgencyWorkflowState.Name))
                //.ForMember(vm => vm.Status, Dm => Dm.MapFrom(dModel => WorkflowStateFactory.GetCollectionAgencyStatus(dModel.AgencyWorkflowState)));
                .ForMember(o => o.Status, opt => opt.MapFrom(o => o.AgencyWorkflowState.Name.Replace("Agency", "")));
        }
    }
}