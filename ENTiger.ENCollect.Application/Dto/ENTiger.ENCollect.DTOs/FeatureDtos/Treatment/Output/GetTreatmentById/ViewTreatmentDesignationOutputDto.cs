using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class ViewTreatmentDesignationOutputDto
    {
        [StringLength(50)]
        public string Id { get; set; }

        public string TreatmentId { get; set; }

        [StringLength(32)]
        public string DepartmentId { get; set; }

        [StringLength(32)]
        public string DesignationId { get; set; }

        public bool? IsDeleted { get; set; }
    }
}