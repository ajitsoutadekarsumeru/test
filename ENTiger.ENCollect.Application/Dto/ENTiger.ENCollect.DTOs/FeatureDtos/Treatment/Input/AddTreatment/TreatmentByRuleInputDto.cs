using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class TreatmentByRuleInputDto
    {
        [StringLength(50)]
        public string? Id { get; set; }

        [StringLength(50)]
        public string? DepartmentId { get; set; }

        [StringLength(50)]
        public string? DesignationId { get; set; }

        [StringLength(50)]
        public string? Rule { get; set; }

        public bool? IsDeleted { get; set; }
    }
}