using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CollectionAgencyDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public CollectionAgencyDetailsMapperConfiguration() : base()
        {
            CreateMap<Agency, CollectionAgencyDetailsDto>()
            .ForMember(vm => vm.ProfileIdentification, Dm => Dm.MapFrom(dModel => dModel.AgencyIdentifications))
            .ForMember(vm => vm.CollectionAgencyType, Dm => Dm.MapFrom(dModel => dModel.AgencyType.MainType))
            .ForMember(vm => vm.CollectionAgencySubType, Dm => Dm.MapFrom(dModel => dModel.AgencyType.SubType))
            .ForMember(vm => vm.CollectionAgencyTypeId, Dm => Dm.MapFrom(dModel => dModel.AgencyType.Id))
            .ForMember(vm => vm.CreditAccountDetails, Dm => Dm.MapFrom(dModel => dModel.CreditAccountDetails))
            .ForMember(vm => vm.AgencyCode, Dm => Dm.MapFrom(dModel => dModel.CustomId));

            CreateMap<AgencyIdentification, AgencyIdentificationDto>()
            .ForMember(vm => vm.IdentificationTypeId, Dm => Dm.MapFrom(dModel => dModel.TFlexIdentificationTypeId))
            .ForMember(vm => vm.IdentificationDocTypeId, Dm => Dm.MapFrom(dModel => dModel.TFlexIdentificationDocTypeId))
            .ForMember(vm => vm.IdentificationDocId, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(vm => vm.ProfileIdentificationDoc, Dm => Dm.MapFrom(dModel => dModel.TFlexIdentificationDocs));

            CreateMap<CreditAccountDetails, CreditAccountDetailsDto>()
            .ForMember(vm => vm.IfscCode, Dm => Dm.MapFrom(dModel => dModel.BankBranch.Code))
            .ForMember(vm => vm.BankName, Dm => Dm.MapFrom(dModel => dModel.BankBranch.Bank.Name))
            .ForMember(vm => vm.BankId, Dm => Dm.MapFrom(dModel => dModel.BankBranch.BankId));

            CreateMap<AgencyIdentificationDoc, AgencyIdentificationDocDto>();

            CreateMap<AgencyWorkflowState, AgencyChangeLogInfoDto>()
                .ForMember(o => o.ChangedByUserId, opt => opt.MapFrom(o => o.StateChangedBy))
                .ForMember(o => o.Status, opt => opt.MapFrom(o => o.Name))
                .ForMember(o => o.Remarks, opt => opt.MapFrom(o => o.Remarks))
                .ForMember(o => o.LastModifiedOn, opt => opt.MapFrom(o => o.StateChangedDate.GetValueOrDefault().DateTime));
        }
    }
}