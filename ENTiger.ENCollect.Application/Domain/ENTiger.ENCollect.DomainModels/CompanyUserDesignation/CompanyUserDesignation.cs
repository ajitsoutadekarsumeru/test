using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserDesignation : DomainModelBridge
    {
        protected readonly ILogger<CompanyUserDesignation> _logger;

        protected CompanyUserDesignation()
        {
        }

        public CompanyUserDesignation(ILogger<CompanyUserDesignation> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? CompanyUserId { get; set; }

        public CompanyUser CompanyUser { get; set; }

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