using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class PrimaryUnAllocationFileDtoWithId : PrimaryUnAllocationFileDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}