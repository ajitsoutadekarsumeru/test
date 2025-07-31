using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class SecondaryAllocationFileDtoWithId : SecondaryAllocationFileDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}