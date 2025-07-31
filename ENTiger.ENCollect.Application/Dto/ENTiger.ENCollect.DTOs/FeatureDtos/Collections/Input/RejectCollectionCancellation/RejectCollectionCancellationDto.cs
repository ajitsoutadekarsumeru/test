using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class RejectCollectionCancellationDto : DtoBridge
    {
        [Required]
        public ICollection<string> ReceiptIds { get; set; }

        [StringLength(200, ErrorMessage = "Remarks value cannot exceed {1} characters")]
        public string Remarks { get; set; }
    }
}