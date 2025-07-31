using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPerformanceBandMaster : DomainModelBridge
    {
        protected readonly ILogger<UserPerformanceBandMaster> _logger;

        protected UserPerformanceBandMaster()
        {
        }

        public UserPerformanceBandMaster(ILogger<UserPerformanceBandMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string Name { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}