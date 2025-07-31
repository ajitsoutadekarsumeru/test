using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    public partial class DesignationPermissions : DomainModelBridge
    {
        protected readonly ILogger<Designation> _logger;

        public DesignationPermissions()
        {
        }

        public DesignationPermissions(ILogger<Designation> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        [StringLength(32)]
        public string DesignationId { get; set; }
        public Designation Designation { get; set; }

        [StringLength(32)]
        public string PermissionId { get; set; }
        public Permissions Permission { get; set; }
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

