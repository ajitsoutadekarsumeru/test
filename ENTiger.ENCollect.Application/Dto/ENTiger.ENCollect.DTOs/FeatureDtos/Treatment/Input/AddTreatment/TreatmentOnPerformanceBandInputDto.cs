using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class TreatmentOnPerformanceBandInputDto
    {
        [StringLength(50)]
        public string? Id { get; set; }

        [StringLength(30)]
        public string? PerformanceBand { get; set; }

        [StringLength(200)]
        public string? CustomerPersona { get; set; }

        [StringLength(50)]
        public string? Percentage { get; set; }

        public bool? IsDeleted { get; set; }
    }
}