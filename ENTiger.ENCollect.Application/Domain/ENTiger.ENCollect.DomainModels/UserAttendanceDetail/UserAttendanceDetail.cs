using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAttendanceDetail : DomainModelBridge
    {
        protected readonly ILogger<UserAttendanceDetail> _logger;

        protected UserAttendanceDetail()
        {
        }

        public UserAttendanceDetail(ILogger<UserAttendanceDetail> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public double TotalHours { get; set; }
        public DateTime Date { get; set; }

        [StringLength(32)]
        public string? ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}