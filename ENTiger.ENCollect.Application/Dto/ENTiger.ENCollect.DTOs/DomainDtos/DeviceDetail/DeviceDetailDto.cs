using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class DeviceDetailDto : DtoBridge
    {
        [StringLength(100)]
        public string? PreviousIMEI { get; set; }

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
    }
}