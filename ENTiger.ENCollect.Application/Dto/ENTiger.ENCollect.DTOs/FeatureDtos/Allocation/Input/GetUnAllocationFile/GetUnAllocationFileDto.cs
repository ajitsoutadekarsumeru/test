using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class GetUnAllocationFileDto : DtoBridge
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string TransactionId { get; set; }
    }
}
