using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentCopyViaEmailDto : DtoBridge
    {
        [Required]
        public string paymentId { get; set; }
    }
}