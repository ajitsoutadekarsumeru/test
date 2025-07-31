using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public class PaymentGatewayDto : DtoBridge
    {
        #region Attributes

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string MerchantId { get; set; }

        [StringLength(50)]
        public string MerchantKey { get; set; }

        [StringLength(150)]
        public string APIKey { get; set; }

        [StringLength(150)]
        public string ChecksumKey { get; set; }

        [StringLength(500)]
        public string PostURL { get; set; }

        [StringLength(500)]
        public string ReturnURL { get; set; }

        [StringLength(500)]
        public string ServerToServerURL { get; set; }

        [StringLength(500)]
        public string ErrorURL { get; set; }

        [StringLength(500)]
        public string CancelURL { get; set; }

        #endregion Attributes
    }
}