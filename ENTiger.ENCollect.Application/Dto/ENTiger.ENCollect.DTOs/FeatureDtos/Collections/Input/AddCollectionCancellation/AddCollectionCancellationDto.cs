using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddCollectionCancellationDto : DtoBridge
    {
        [Required]
        public ICollection<string> ReceiptIds { get; set; }

        [StringLength(300, ErrorMessage = "Remarks value cannot exceed {1} characters")]
        public string ReceiptRemarks { get; set; }
    }
}