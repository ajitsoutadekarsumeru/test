using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class AreaDtoWithId : AreaDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}