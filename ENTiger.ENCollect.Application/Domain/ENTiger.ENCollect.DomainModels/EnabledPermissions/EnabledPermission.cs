using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EnabledPermission : DomainModelBridge
    {
        protected readonly ILogger<EnabledPermission> _logger;

        public EnabledPermission()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<EnabledPermission>>();
        }

        public EnabledPermission(ILogger<EnabledPermission> logger)
        {
            _logger = logger;
        }

        #region "Attributes"
        [StringLength(32)]
        public string PermissionId { get; set; }

        public Permissions Permission { get; set; }
        [StringLength(32)]
        public string PermissionSchemeId { get; set; }
        public PermissionSchemes PermissionScheme { get; set; }
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
