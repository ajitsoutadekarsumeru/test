using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserVerificationCodes : DomainModelBridge
    {
        protected readonly ILogger<UserVerificationCodes> _logger;

        public UserVerificationCodes()
        {
        }

        public UserVerificationCodes(ILogger<UserVerificationCodes> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? UserId { get; set; }

        [StringLength(50)]
        public string? ShortVerificationCode { get; set; }

        [StringLength(4000)]
        public string? VerificationCode { get; set; }

        [StringLength(32)]
        public string? UserVerificationCodeTypeId { get; set; }

        public UserVerificationCodeTypes UserVerificationCodeType { get; set; }

        [StringLength(50)]
        public string? TransactionID { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}