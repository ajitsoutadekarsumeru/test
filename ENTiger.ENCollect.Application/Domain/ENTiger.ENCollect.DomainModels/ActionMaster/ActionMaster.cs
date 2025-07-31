using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class ActionMaster : DomainModelBridge
    {
        protected readonly ILogger<ActionMaster> _logger;

        protected ActionMaster()
        {
        }

        public ActionMaster(ILogger<ActionMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? Name { get; set; }

        public bool HasAccess { get; set; }

        [StringLength(32)]
        public string? SubMenuMasterID { get; set; }

        public SubMenuMaster SubMenuMaster { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}