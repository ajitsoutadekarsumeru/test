using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    [Table("PaymentGateways")]
    public class PaymentGateway : DomainModelBridge
    {
        #region Constructors

        public PaymentGateway()
        {
        }

        #endregion Constructors

        #region Attributes

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? MerchantId { get; set; }

        [StringLength(50)]
        public string? MerchantKey { get; set; }

        [StringLength(150)]
        public string? APIKey { get; set; }

        [StringLength(150)]
        public string? ChecksumKey { get; set; }

        [StringLength(500)]
        public string? PostURL { get; set; }

        [StringLength(500)]
        public string? ReturnURL { get; set; }

        [StringLength(500)]
        public string? ServerToServerURL { get; set; }

        [StringLength(500)]
        public string? ErrorURL { get; set; }

        [StringLength(500)]
        public string? CancelURL { get; set; }

        #endregion Attributes
    }
}