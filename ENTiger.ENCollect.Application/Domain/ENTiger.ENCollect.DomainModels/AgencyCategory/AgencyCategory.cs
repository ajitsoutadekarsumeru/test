using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyCategory : DomainModelBridge
    {
        protected readonly ILogger<AgencyCategory> _logger;

        protected AgencyCategory()
        {
        }

        public AgencyCategory(ILogger<AgencyCategory> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Name { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}