using ENTiger.ENCollect.CommonModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgentDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AgentDetailsMapperConfiguration() : base()
        {
            CreateMap<AgencyUser, AgentDetailsDto>()
                .ForMember(o => o.AgencyFirstName, opt => opt.MapFrom(o => o.Agency.FirstName))
                .ForMember(o => o.AgencyLastName, opt => opt.MapFrom(o => o.Agency.LastName))
                .ForMember(o => o.AgencyCode, opt => opt.MapFrom(o => o.Agency.CustomId))
                .ForMember(o => o.Status, opt => opt.MapFrom(o => o.AgencyUserWorkflowState.Name))
                .ForMember(o => o.Roles, opt => opt.MapFrom(o => o.Designation.Where(a => !a.IsDeleted)))
                .ForMember(o => o.ProductScopes, opt => opt.MapFrom(o => o.ProductScopes.Where(a => !a.IsDeleted)))
                .ForMember(o => o.GeoScopes, opt => opt.MapFrom(o => o.GeoScopes.Where(a => !a.IsDeleted)))
                .ForMember(o => o.BucketScopes, opt => opt.MapFrom(o => o.BucketScopes.Where(a => !a.IsDeleted)))
                .ForMember(o => o.PlaceOfWork, opt => opt.MapFrom(o => o.PlaceOfWork.Where(a => !a.IsDeleted)))
                .ForMember(o => o.Languages, opt => opt.MapFrom(o => o.Languages.Where(a => !a.IsDeleted)))
                .ForMember(o => o.PhysicalIDcardNumber, opt => opt.MapFrom(o => o.IdCardNumber))
                .ForMember(o => o.WalletLimit, opt => opt.MapFrom(o => o.Wallet != null ? o.Wallet.WalletLimit : 0))
                .ForMember(o => o.UserType, opt => opt.MapFrom(o => o.UserType));
            CreateMap<CreditAccountDetails, AgentBankDetailsDto>()
                .ForMember(vm => vm.IfscCode, Dm => Dm.MapFrom(dModel => dModel.BankBranch.Code))
                .ForMember(vm => vm.BankName, Dm => Dm.MapFrom(dModel => dModel.BankBranch.BankId));

            CreateMap<AgencyUserDesignation, UserDesignationOutputApiModel>();
            CreateMap<UserProductScope, UserProductScopeDto>();
            CreateMap<UserGeoScope, UserGeoScopeDto>();
            CreateMap<UserBucketScope, UserBucketScopeDto>();
            CreateMap<AgencyUserPlaceOfWork, AgencyUserPlaceOfWorkDto>();
            CreateMap<Address, AddressDto>();
            CreateMap<CreditAccountDetails, AgentBankDetailsDto>();
            CreateMap<Language, LanguageDto>();
            CreateMap<UserCustomerPersona, UserCustomerPersonaDto>();
            CreateMap<UserPerformanceBand, UserPerformanceBandDto>();

            CreateMap<AgencyUserWorkflowState, AgentChangeLogInfoDto>()
                .ForMember(o => o.ChangedByUserId, opt => opt.MapFrom(o => o.StateChangedBy))
                .ForMember(o => o.Status, opt => opt.MapFrom(o => o.Name))
                .ForMember(o => o.Remarks, opt => opt.MapFrom(o => o.Remarks))
                .ForMember(o => o.LastModifiedOn, opt => opt.MapFrom(o => o.StateChangedDate.GetValueOrDefault().DateTime));
        }
    }
}