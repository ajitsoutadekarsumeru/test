using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddTreatmentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddTreatmentMapperConfiguration() : base()
        {
            CreateMap<AddTreatmentDto, Treatment>()
            .ForMember(cm => cm.Name, Dm => Dm.MapFrom(dModel => dModel.Name))
            .ForMember(cm => cm.Description, Dm => Dm.MapFrom(dModel => dModel.Description))
            .ForMember(cm => cm.Mode, Dm => Dm.MapFrom(dModel => dModel.Mode))
            .ForMember(cm => cm.subTreatment, Dm => Dm.MapFrom(dModel => dModel.subTreatment));

            CreateMap<TreatmentAndSegmentMapping, TreatmentAndSegmentInputDto>().ReverseMap()
            .ForMember(cm => cm.SegmentId, Dm => Dm.MapFrom(dModel => dModel.SegmentId));
            CreateMap<TreatmentOnPOS, TreatmentOnPOSInputDto>().ReverseMap()
            .ForMember(cm => cm.DepartmentId, Dm => Dm.MapFrom(dModel => dModel.DepartmentId))
            .ForMember(cm => cm.DesignationId, Dm => Dm.MapFrom(dModel => dModel.DesignationId))
            .ForMember(cm => cm.Percentage, Dm => Dm.MapFrom(dModel => dModel.Percentage))
            .ForMember(cm => cm.AllocationId, Dm => Dm.MapFrom(dModel => dModel.AllocationId));
            CreateMap<TreatmentOnAccount, TreatmentOnAccountInputDto>().ReverseMap()
            .ForMember(cm => cm.DepartmentId, Dm => Dm.MapFrom(dModel => dModel.DepartmentId))
            .ForMember(cm => cm.DesignationId, Dm => Dm.MapFrom(dModel => dModel.DesignationId))
            .ForMember(cm => cm.Percentage, Dm => Dm.MapFrom(dModel => dModel.Percentage))
            .ForMember(cm => cm.AllocationId, Dm => Dm.MapFrom(dModel => dModel.AllocationId));
            CreateMap<RoundRobinTreatment, RoundRobinTreatmentInputDto>().ReverseMap()
            .ForMember(cm => cm.AllocationId, Dm => Dm.MapFrom(dModel => dModel.AllocationId));
            CreateMap<TreatmentByRule, TreatmentByRuleInputDto>().ReverseMap()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.DepartmentId, Dm => Dm.MapFrom(dModel => dModel.DepartmentId))
            .ForMember(cm => cm.DesignationId, Dm => Dm.MapFrom(dModel => dModel.DesignationId))
            .ForMember(cm => cm.Rule, Dm => Dm.MapFrom(dModel => dModel.Rule));

            CreateMap<TreatmentOnUpdateTrail, TreatmentOnUpdateTrailInputDto>().ReverseMap()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.DispositionCodeGroup, Dm => Dm.MapFrom(dModel => dModel.DispositionCodeGroup))
            .ForMember(cm => cm.DispositionCode, Dm => Dm.MapFrom(dModel => dModel.DispositionCode))
            .ForMember(cm => cm.NextActionDate, Dm => Dm.MapFrom(dModel => dModel.NextActionDate))
            .ForMember(cm => cm.PTPAmount, Dm => Dm.MapFrom(dModel => dModel.PTPAmount));

            CreateMap<TreatmentOnCommunication, TreatmentOnCommunicationInputDto>().ReverseMap()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.CommunicationType, Dm => Dm.MapFrom(dModel => dModel.CommunicationType))
            .ForMember(cm => cm.CommunicationTemplateId, Dm => Dm.MapFrom(dModel => dModel.CommunicationTemplateId))
            .ForMember(cm => cm.CommunicationMobileNumberType, Dm => Dm.MapFrom(dModel => dModel.CommunicationMobileNumberType));

            CreateMap<TreatmentQualifyingStatus, TreatmentQualifyingStatusInputDto>().ReverseMap()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId))
            .ForMember(cm => cm.SubTreatmentId, Dm => Dm.MapFrom(dModel => dModel.SubTreatmentId))
            .ForMember(cm => cm.Status, Dm => Dm.MapFrom(dModel => dModel.Status));

            CreateMap<TreatmentOnPerformanceBand, TreatmentOnPerformanceBandInputDto>().ReverseMap()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.PerformanceBand, Dm => Dm.MapFrom(dModel => dModel.PerformanceBand))
            .ForMember(cm => cm.CustomerPersona, Dm => Dm.MapFrom(dModel => dModel.CustomerPersona))
            .ForMember(cm => cm.Percentage, Dm => Dm.MapFrom(dModel => dModel.Percentage));

            CreateMap<TreatmentDesignation, TreatmentDesignationInputDto>().ReverseMap()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.DepartmentId, Dm => Dm.MapFrom(dModel => dModel.DepartmentId))
            .ForMember(cm => cm.DesignationId, Dm => Dm.MapFrom(dModel => dModel.DesignationId));

            CreateMap<TreatmentQualifyingStatus, TreatmentQualifyingStatusInputDto>().ReverseMap()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.Status, Dm => Dm.MapFrom(dModel => dModel.Status));

            CreateMap<SubTreatment, SubTreatmentsInputDto>().ReverseMap()
            .ForMember(cm => cm.TreatmentType, Dm => Dm.MapFrom(dModel => dModel.TreatmentType))
            .ForMember(cm => cm.AllocationType, Dm => Dm.MapFrom(dModel => dModel.AllocationType))
            .ForMember(cm => cm.StartDay, Dm => Dm.MapFrom(dModel => dModel.StartDay))
            .ForMember(cm => cm.EndDay, Dm => Dm.MapFrom(dModel => dModel.EndDay))
            .ForMember(cm => cm.Order, Dm => Dm.MapFrom(dModel => dModel.Order))
            .ForMember(cm => cm.POSCriteria, Dm => Dm.MapFrom(dModel => dModel.POSCriteria))
            .ForMember(cm => cm.AccountCriteria, Dm => Dm.MapFrom(dModel => dModel.AccountCriteria))
            .ForMember(cm => cm.RoundRobinCriteria, Dm => Dm.MapFrom(dModel => dModel.RoundRobinCriteria))
            .ForMember(cm => cm.TreatmentByRule, Dm => Dm.MapFrom(dModel => dModel.TreatmentByRule))
            .ForMember(cm => cm.TreatmentOnUpdateTrail, Dm => Dm.MapFrom(dModel => dModel.UpdateTrail))
            .ForMember(cm => cm.TreatmentOnCommunication, Dm => Dm.MapFrom(dModel => dModel.Communication))
            .ForMember(cm => cm.PerformanceBand, Dm => Dm.MapFrom(dModel => dModel.PerformanceBand))
            .ForMember(cm => cm.Designation, Dm => Dm.MapFrom(dModel => dModel.Designation))
            .ForMember(cm => cm.ScriptToPersueCustomer, Dm => Dm.MapFrom(dModel => dModel.ScriptToPersueCustomer))
            .ForMember(cm => cm.QualifyingCondition, Dm => Dm.MapFrom(dModel => dModel.QualifyingCondition))
            .ForMember(cm => cm.PreSubtreatmentOrder, Dm => Dm.MapFrom(dModel => dModel.PreSubtreatmentOrder))
            .ForMember(cm => cm.QualifyingStatus, Dm => Dm.MapFrom(dModel => dModel.QualifyingStatus))
            .ForMember(cm => cm.DeliveryStatus, Dm => Dm.MapFrom(dModel => dModel.DeliveryStatus));
        }
    }
}