using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DispositionCodeMasterDtoWithId : DispositionCodeMasterDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}