using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class ViewSubTreatmentOutputDto
    {
        public string Id { get; set; }

        public int? Order { get; set; }

        [StringLength(100)]
        public string TreatmentType { get; set; }

        [StringLength(100)]
        public string AllocationType { get; set; }

        [StringLength(20)]
        public string StartDay { get; set; }

        [StringLength(20)]
        public string EndDay { get; set; }

        public List<ViewTreatmentByRuleDto> TreatmentByRule { get; set; }
        public List<ViewTreatmentOnAccountDto> AccountCriteria { get; set; }
        public List<ViewTreatmentOnPosDto> POSCriteria { get; set; }
        public List<ViewTreatmentRoundRobinTreatmentDto> RoundRobinCriteria { get; set; }

        public List<ViewTreatmentOnPerformanceBandDto> PerformanceBand { get; set; }

        public List<ViewTreatmentDesignationOutputDto> Designation { get; set; }

        public ViewTreatmentOnUpdateTrailOutputDto UpdateTrail { get; set; }

        public ViewTreatmentOnCommunicationOutputDto Communication { get; set; }

        public bool? IsDeleted { get; set; }

        public string ScriptToPersueCustomer { get; set; }

        [StringLength(20)]
        public string QualifyingCondition { get; set; }

        public int? PreSubtreatmentOrder { get; set; }

        [StringLength(100)]
        public string QualifyingStatus { get; set; }

        public List<ViewTreatmentOnQualifyingStatusOutputDto> DeliveryStatus { get; set; }
    }
}