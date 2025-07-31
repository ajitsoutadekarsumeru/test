using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPersonaMaster : DomainModelBridge
    {
        protected readonly ILogger<UserPersonaMaster> _logger;

        protected UserPersonaMaster()
        {
        }

        public UserPersonaMaster(ILogger<UserPersonaMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Code { get; set; }

        #endregion "Public"

        #region "Protected"
        #endregion "Protected"

        #region "Private"
        #endregion "Private"

        #endregion "Attributes"

        #region "Private Methods"
        #endregion "Private Methods"
    }
}