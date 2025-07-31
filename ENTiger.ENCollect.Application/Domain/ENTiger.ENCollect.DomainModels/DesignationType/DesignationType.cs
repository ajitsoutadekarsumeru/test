using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DesignationType : DomainModelBridge
    {
        protected readonly ILogger<DesignationType> _logger;

        protected DesignationType()
        {
        }

        public DesignationType(ILogger<DesignationType> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? Name { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}