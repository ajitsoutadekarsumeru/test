using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class AddTreatmentDto : DtoBridge
    {
        [Required]
        [StringLength(300)]
        public string? Name { get; set; }

        [StringLength(100, ErrorMessage = "Please enter maximum 100 characters in description")]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string? Mode { get; set; }

        [StringLength(50)]
        public string? PaymentStatusToStop { get; set; }

        public ICollection<TreatmentAndSegmentInputDto> segmentMapping { get; set; }

        public ICollection<SubTreatmentsInputDto> subTreatment { get; set; }
    }
}