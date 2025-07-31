using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DepartmentTypeDtoWithId : DepartmentTypeDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}