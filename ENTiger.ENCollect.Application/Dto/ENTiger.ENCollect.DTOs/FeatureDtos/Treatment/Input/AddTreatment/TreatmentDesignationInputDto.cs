using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class TreatmentDesignationInputDto
    {
        [StringLength(50)]
        public string? Id { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }
    }
}