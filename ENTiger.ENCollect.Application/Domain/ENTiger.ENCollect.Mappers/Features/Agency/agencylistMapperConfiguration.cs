using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class agencylistMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public agencylistMapperConfiguration() : base()
        {
            CreateMap<Agency, agencylistDto>()
                 .ForMember(vm => vm.AgencyCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                 .ForMember(vm => vm.AgencyWFState, Dm => Dm.MapFrom(dModel => dModel.AgencyWorkflowState.Name));
        }
    }
}