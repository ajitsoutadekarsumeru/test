using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentLinkDto : DtoBridge
    {
        [Required]
        public string Accountno { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        [Required]
        public string OnlinePayPartnerName { get; set; }

        [StringLength(50)]
        public string? MobileNo { get; set; }

        [StringLength(200)]
        public string? EMailId { get; set; }
    }
}