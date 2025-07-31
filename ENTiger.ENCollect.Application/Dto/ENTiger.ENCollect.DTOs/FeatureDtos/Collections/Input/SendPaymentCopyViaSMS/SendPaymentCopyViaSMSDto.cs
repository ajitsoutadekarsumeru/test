using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentCopyViaSMSDto : DtoBridge
    {
        [Required]
        public string paymentId { get; set; }
    }
}