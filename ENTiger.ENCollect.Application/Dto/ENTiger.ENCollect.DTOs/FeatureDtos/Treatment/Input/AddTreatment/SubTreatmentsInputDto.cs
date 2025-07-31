using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class SubTreatmentsInputDto
    {
        public int? Order { get; set; }

        [StringLength(50)]
        public string? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? TreatmentType { get; set; }

        //[Required]
        [StringLength(100)]
        public string? AllocationType { get; set; }

        [StringLength(20)]
        public string? StartDay { get; set; }

        [StringLength(20)]
        public string? EndDay { get; set; }

        public ICollection<TreatmentOnPOSInputDto> POSCriteria { get; set; }

        public ICollection<TreatmentOnAccountInputDto> AccountCriteria { get; set; }

        public ICollection<RoundRobinTreatmentInputDto> RoundRobinCriteria { get; set; }

        public List<TreatmentByRuleInputDto> TreatmentByRule { get; set; }

        public List<TreatmentOnPerformanceBandInputDto> PerformanceBand { get; set; }

        public List<TreatmentDesignationInputDto> Designation { get; set; }

        public TreatmentOnUpdateTrailInputDto? UpdateTrail { get; set; }

        public TreatmentOnCommunicationInputDto? Communication { get; set; }

        public bool? IsDeleted { get; set; }

        public string? ScriptToPersueCustomer { get; set; }

        [StringLength(20)]
        public string? QualifyingCondition { get; set; }

        public int? PreSubtreatmentOrder { get; set; }

        public List<TreatmentQualifyingStatusDto> DeliveryStatus { get; set; }

        [StringLength(100)]
        public string? QualifyingStatus { get; set; }
    }
}