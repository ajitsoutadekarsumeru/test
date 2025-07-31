using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class UserDesignationDto : DtoBridge
    {
        public string? Id { get; set; }

        [Required]
        public string? DepartmentId { get; set; }

        [Required]
        public string? DesignationId { get; set; }
        public string? Role { get; set; }        
        public bool IsDeleted { get; set; }

        public bool IsPrimaryDesignation { get; set; }
    }
}