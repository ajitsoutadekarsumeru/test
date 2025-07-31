using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DailyActivityDetail : DomainModelBridge
    {
        protected readonly ILogger<DeviceDetail> _logger;

        public DailyActivityDetail()
        {
        }

        public DailyActivityDetail(ILogger<DeviceDetail> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public DateTime? ActivityTs { get; set; }

        [StringLength(100)]
        public string? ActivityType { get; set; }

        [StringLength(300)]
        public string? Location { get; set; }

        [StringLength(50)]
        public string? ActivityWeekDay { get; set; }

        public int? ActivityDayNumber { get; set; }
        public int? ActivityMonth { get; set; }
        public int? ActivityYear { get; set; }

        [StringLength(250)]
        public string? State { get; set; }

        public double? Lat { get; set; }

        [StringLength(250)]
        public string? Department { get; set; }

        [StringLength(50)]
        public string? Mobile { get; set; }

        [StringLength(200)]
        public string? Name { get; set; }

        public bool? StaffOrAgent { get; set; }

        [StringLength(250)]
        public string? EmpanalmentStatus { get; set; }

        [StringLength(250)]
        public string? Designation { get; set; }

        [StringLength(200)]
        public string? Agency { get; set; }

        [StringLength(200)]
        public string? Branch { get; set; }

        [StringLength(200)]
        public string? UserId { get; set; }

        [StringLength(100)]
        public string? EmailId { get; set; }

        public double? Long { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}