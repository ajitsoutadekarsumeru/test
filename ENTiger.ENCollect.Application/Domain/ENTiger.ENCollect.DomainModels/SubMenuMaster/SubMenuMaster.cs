using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SubMenuMaster : DomainModelBridge
    {
        protected readonly ILogger<SubMenuMaster> _logger;

        protected SubMenuMaster()
        {
        }

        public SubMenuMaster(ILogger<SubMenuMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? SubMenuName { get; set; }

        public bool hasAccess { get; set; }

        [StringLength(32)]
        public string? MenuMasterId { get; set; }

        public MenuMaster MenuMaster { get; set; }

        public List<ActionMaster>? Actions { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}