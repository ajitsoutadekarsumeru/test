using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAccessRights : AccessRight
    {
        protected readonly ILogger<UserAccessRights> _logger;

        protected UserAccessRights()
        {
        }

        public UserAccessRights(ILogger<UserAccessRights> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(10)]
        public string? MethodType { get; set; }

        [StringLength(250)]
        public string? Route { get; set; }

        [StringLength(32)]
        public string? MenuMasterId { get; set; }

        public MenuMaster MenuMaster { get; set; }
        public ActionMaster ActionMaster { get; set; }
        public SubMenuMaster SubMenuMaster { get; set; }
        public bool IsMobile { get; set; }
        public bool IsFrontEnd { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}