using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPerformanceBand : DomainModelBridge
    {
        protected readonly ILogger<UserPerformanceBand> _logger;

        protected UserPerformanceBand()
        {
        }

        public UserPerformanceBand(ILogger<UserPerformanceBand> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(32)]
        public string? CompanyUserId { get; set; }

        public CompanyUser CompanyUser { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}