using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SecondaryUnAllocationByBatchDto : DtoBridge
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }
        public string? UnAllocationType { get; set; }
    }
}