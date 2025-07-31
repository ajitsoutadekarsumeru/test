using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class PrimaryAllocationFileDtoWithId : PrimaryAllocationFileDto
    {
        [StringLength(32)]
        public string Id { get; set; }
    }
}