using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class MenuMaster : DomainModelBridge
    {
        protected readonly ILogger<MenuMaster> _logger;

        protected MenuMaster()
        {
        }

        public MenuMaster(ILogger<MenuMaster> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? MenuName { get; set; }

        [StringLength(100)]
        public string? Etc { get; set; }

        public List<SubMenuMaster>? SubMenus { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}