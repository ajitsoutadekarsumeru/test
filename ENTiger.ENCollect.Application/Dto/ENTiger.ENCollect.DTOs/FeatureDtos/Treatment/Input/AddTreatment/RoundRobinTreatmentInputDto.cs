using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class RoundRobinTreatmentInputDto
    {
        [StringLength(50)]
        public string? Id { get; set; }

        [StringLength(50)]
        public string? AllocationId { get; set; }

        [StringLength(100)]
        public string? AllocationName { get; set; }

        public bool? IsDeleted { get; set; }
    }
}