using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeviceDetail : DomainModelBridge
    {
        protected readonly ILogger<DeviceDetail> _logger;

        protected DeviceDetail()
        {
        }

        public DeviceDetail(ILogger<DeviceDetail> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? OldIMEI { get; set; }

        [StringLength(100)]
        public string? IMEI { get; set; }

        [StringLength(50)]
        public string? UserId { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        public string? OTP { get; set; }

        public DateTimeOffset OTPTimeStamp { get; set; }

        public bool IsVerified { get; set; }

        public DateTimeOffset? VerifiedDate { get; set; }

        public int OTPCount { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}