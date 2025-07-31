using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SecondaryUnAllocationFileDtoWithId : SecondaryUnAllocationFileDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}