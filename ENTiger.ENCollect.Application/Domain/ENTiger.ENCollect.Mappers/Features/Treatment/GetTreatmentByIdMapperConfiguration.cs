using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTreatmentByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetTreatmentByIdMapperConfiguration() : base()
        {
            CreateMap<Treatment, GetTreatmentByIdDto>()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.subTreatment, Dm => Dm.MapFrom(dModel => dModel.subTreatment));

            CreateMap<SubTreatment, ViewSubTreatmentOutputDto>()
            .ForMember(cm => cm.Id, Dm => Dm.MapFrom(dModel => dModel.Id))
            .ForMember(cm => cm.TreatmentType, Dm => Dm.MapFrom(dModel => dModel.TreatmentType))
            .ForMember(cm => cm.StartDay, Dm => Dm.MapFrom(dModel => dModel.StartDay))
            .ForMember(cm => cm.EndDay, Dm => Dm.MapFrom(dModel => dModel.EndDay))
            .ForMember(cm => cm.Order, Dm => Dm.MapFrom(dModel => dModel.Order))
            .ForMember(cm => cm.UpdateTrail, Dm => Dm.MapFrom(dModel => dModel.TreatmentOnUpdateTrail))
            .ForMember(cm => cm.Communication, Dm => Dm.MapFrom(dModel => dModel.TreatmentOnCommunication))

            .ForMember(cm => cm.AllocationType, Dm => Dm.MapFrom(dModel => dModel.AllocationType))
            .ForMember(cm => cm.AccountCriteria, Dm => Dm.MapFrom(dModel => dModel.AccountCriteria))
            .ForMember(cm => cm.POSCriteria, Dm => Dm.MapFrom(dModel => dModel.POSCriteria))
            .ForMember(cm => cm.RoundRobinCriteria, Dm => Dm.MapFrom(dModel => dModel.RoundRobinCriteria))
            .ForMember(cm => cm.TreatmentByRule, Dm => Dm.MapFrom(dModel => dModel.TreatmentByRule))
            .ForMember(cm => cm.PerformanceBand, Dm => Dm.MapFrom(dModel => dModel.PerformanceBand))
            .ForMember(cm => cm.ScriptToPersueCustomer, Dm => Dm.MapFrom(dModel => dModel.ScriptToPersueCustomer))
            .ForMember(cm => cm.QualifyingCondition, Dm => Dm.MapFrom(dModel => dModel.QualifyingCondition))
            .ForMember(cm => cm.QualifyingStatus, Dm => Dm.MapFrom(dModel => dModel.QualifyingStatus))
            .ForMember(cm => cm.PreSubtreatmentOrder, Dm => Dm.MapFrom(dModel => dModel.PreSubtreatmentOrder))
            .ForMember(cm => cm.DeliveryStatus, Dm => Dm.MapFrom(dModel => dModel.DeliveryStatus));

            CreateMap<TreatmentByRule, ViewTreatmentByRuleDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentOnAccount, ViewTreatmentOnAccountDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentOnPOS, ViewTreatmentOnPosDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentAndSegmentMapping, ViewTreatmentSegmentMappingTreatmentOutputDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId))
            .ForMember(cm => cm.SegmentName, Dm => Dm.MapFrom(dModel => dModel.Segment.Name));

            CreateMap<RoundRobinTreatment, ViewTreatmentRoundRobinTreatmentDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentOnUpdateTrail, ViewTreatmentOnUpdateTrailOutputDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentOnCommunication, ViewTreatmentOnCommunicationOutputDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentOnPerformanceBand, ViewTreatmentOnPerformanceBandDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentDesignation, ViewTreatmentDesignationOutputDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));

            CreateMap<TreatmentQualifyingStatus, ViewTreatmentOnQualifyingStatusOutputDto>()
            .ForMember(cm => cm.TreatmentId, Dm => Dm.MapFrom(dModel => dModel.TreatmentId));
        }
    }
}