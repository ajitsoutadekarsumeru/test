using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class UpdateTreatmentDto : DtoBridge
    {
        [Required]
        [StringLength(50)]
        public string? Id { get; set; }

        [Required]
        [StringLength(300)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string? Mode { get; set; }

        [StringLength(50)]
        public string? PaymentStatusToStop { get; set; }

        public List<TreatmentAndSegmentInputDto>? segmentMapping { get; set; }

        public List<SubTreatmentsInputDto>? subTreatment { get; set; }
    }
}