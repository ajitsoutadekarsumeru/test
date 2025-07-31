using ENTiger.ENCollect.CommonModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class searchAgentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public searchAgentMapperConfiguration() : base()
        {
            CreateMap<AgencyUser, searchAgentDto>()
                .ForMember(vm => vm.AgentCode, Dm => Dm.MapFrom(dModel => dModel.CustomId))
                .ForMember(vm => vm.AgentName, Dm => Dm.MapFrom(dModel => dModel.FirstName + " " + dModel.LastName))
                .ForMember(vm => vm.AgencyName, Dm => Dm.MapFrom(dModel => dModel.Agency.FirstName + " " + dModel.Agency.LastName))
                .ForMember(vm => vm.AuthorizationCardExpiry, DM => DM.MapFrom(dmodel => dmodel.AuthorizationCardExpiryDate))
                .ForMember(vm => vm.PhoneNumber, DM => DM.MapFrom(dmodel => dmodel.PrimaryMobileNumber))
                .ForMember(vm => vm.Status, Dm => Dm.MapFrom(dModel => WorkflowStateFactory.GetCollectionAgencyUserstatus(dModel.AgencyUserWorkflowState.Name)))
                .ForMember(vm => vm.Roles, Dm => Dm.MapFrom(dModel => dModel.Designation.OrderByDescending(D => D.IsPrimaryDesignation)))
                .ForMember(vm => vm.WalletLimit, Dm => Dm.MapFrom(dModel => dModel.Wallet != null ? dModel.Wallet.WalletLimit : 0));

            CreateMap<AgencyUserDesignation, UserDesignationOutputApiModel>();
        }
    }
}