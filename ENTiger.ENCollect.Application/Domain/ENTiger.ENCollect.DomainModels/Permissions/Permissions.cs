using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect
{
    public partial class Permissions : DomainModelBridge
    {
        protected readonly ILogger<Designation> _logger;

        public Permissions()
        {
        }

        public Permissions(ILogger<Designation> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string Section { get; set; }

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
