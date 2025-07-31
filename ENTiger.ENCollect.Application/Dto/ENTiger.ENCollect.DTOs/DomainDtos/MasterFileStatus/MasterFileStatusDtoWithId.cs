using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class MasterFileStatusDtoWithId : MasterFileStatusDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}