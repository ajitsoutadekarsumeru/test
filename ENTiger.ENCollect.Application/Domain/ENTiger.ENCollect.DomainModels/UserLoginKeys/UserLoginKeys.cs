using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserLoginKeys : DomainModelBridge
    {
        protected readonly ILogger<UserLoginKeys> _logger;

        protected UserLoginKeys()
        {
        }

        public UserLoginKeys(ILogger<UserLoginKeys> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Key { get; set; }

        public bool IsActive { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}