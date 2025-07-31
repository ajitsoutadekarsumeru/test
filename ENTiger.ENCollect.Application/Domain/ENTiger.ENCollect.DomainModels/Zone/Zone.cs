using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Zone : DomainModelBridge
    {
        protected readonly ILogger<Zone> _logger;

        protected Zone()
        {
        }

        public Zone(ILogger<Zone> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}