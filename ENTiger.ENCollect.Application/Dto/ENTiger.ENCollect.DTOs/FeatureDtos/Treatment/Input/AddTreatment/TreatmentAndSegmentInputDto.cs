using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class TreatmentAndSegmentInputDto
    {
        [StringLength(50)]
        public string? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? SegmentId { get; set; }
    }
}