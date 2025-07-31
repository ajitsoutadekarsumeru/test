using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyType : DomainModelBridge
    {
        protected readonly ILogger<AgencyType> _logger;

        protected AgencyType()
        {
        }

        public AgencyType(ILogger<AgencyType> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? MainType { get; set; }

        [StringLength(50)]
        public string? SubType { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}