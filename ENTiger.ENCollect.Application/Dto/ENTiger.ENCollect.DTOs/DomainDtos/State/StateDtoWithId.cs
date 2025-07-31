using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class StateDtoWithId : StateDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}