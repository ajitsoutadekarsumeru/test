using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class TreatmentQualifyingStatusInputDto
    {
        [StringLength(32)]
        public string Id { get; set; }

        [StringLength(32)]
        public string TreatmentId { get; set; }

        [StringLength(32)]
        public string SubTreatmentId { get; set; }

        [StringLength(200)]
        public string Status { get; set; }
    }
}