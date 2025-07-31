using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DepartmentDtoWithId : DepartmentDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}