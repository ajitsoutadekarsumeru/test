using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DispositionGroupMasterDtoWithId : DispositionGroupMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}