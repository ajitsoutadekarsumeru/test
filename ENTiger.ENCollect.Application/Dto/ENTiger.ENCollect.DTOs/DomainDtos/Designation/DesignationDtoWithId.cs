using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DesignationDtoWithId : DesignationDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}