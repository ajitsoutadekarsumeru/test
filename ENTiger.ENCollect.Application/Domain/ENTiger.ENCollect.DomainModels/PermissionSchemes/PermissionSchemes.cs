using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermissionSchemes : DomainModelBridge
    {
        protected readonly ILogger<PermissionSchemes> _logger;

        protected PermissionSchemes()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<PermissionSchemes>>();
        }

        public PermissionSchemes(ILogger<PermissionSchemes> logger)
        {
            _logger = logger;
        }

        #region "Attributes"
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }

        public List<Designation> Designations { get; set; }
        public List<EnabledPermission> EnabledPermissions { get; set; }
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
