using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermissionSchemeChangeLog : DomainModelBridge
    {
        protected readonly ILogger<PermissionSchemeChangeLog> _logger;

        protected PermissionSchemeChangeLog()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<PermissionSchemeChangeLog>>();
        }

        public PermissionSchemeChangeLog(ILogger<PermissionSchemeChangeLog> logger)
        {
            _logger = logger;
        }
        #region "Attributes"
        [StringLength(32)]
        public string PermissionSchemeId { get; set; }
        [StringLength(2000)]
        public string? AddedPermissions { get; set; }
        [StringLength(2000)]
        public string? RemovedPermissions { get; set; }
        [StringLength(50)]
        public string ChangeType { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        #region "Public"
        #endregion

        #region "Protected"
        #endregion

        #region "Private"
        #endregion

        #endregion

        #region "Private Methods"
        #endregion
    }
}
