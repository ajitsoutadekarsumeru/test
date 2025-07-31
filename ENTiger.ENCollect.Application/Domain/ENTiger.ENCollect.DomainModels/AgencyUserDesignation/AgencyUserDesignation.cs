using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserDesignation : DomainModelBridge
    {
        protected readonly ILogger<AgencyUserDesignation> _logger;

        public AgencyUserDesignation()
        {
        }

        public AgencyUserDesignation(ILogger<AgencyUserDesignation> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? AgencyUserId { get; set; }

        public AgencyUser AgencyUser { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        public Department Department { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }

        public Designation Designation { get; set; }
        public bool IsPrimaryDesignation { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}