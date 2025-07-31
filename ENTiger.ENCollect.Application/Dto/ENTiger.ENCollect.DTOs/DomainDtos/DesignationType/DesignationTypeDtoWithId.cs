using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DesignationTypeDtoWithId : DesignationTypeDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}