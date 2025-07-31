using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAttendanceLog : DomainModelBridge
    {
        protected readonly ILogger<UserAttendanceLog> _logger;

        public UserAttendanceLog()
        {
        }

        public UserAttendanceLog(ILogger<UserAttendanceLog> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string? SessionId { get; set; }
        public bool IsSessionValid { get; set; }

        public double? LogOutLongitude { get; set; }
        public double? LogInLongitude { get; set; }

        public double? LogOutLatitude { get; set; }
        public double? LogInLatitude { get; set; }

        public DateTime? LogInTime { get; set; }
        public DateTime? LogOutTime { get; set; }

        [StringLength(800)]
        public string? GeoLocation { get; set; }

        public bool? IsFirstLogin { get; set; }
        [StringLength(20)]
        public string? TransactionSource { get; set; }
        #endregion

        #endregion

    }
}