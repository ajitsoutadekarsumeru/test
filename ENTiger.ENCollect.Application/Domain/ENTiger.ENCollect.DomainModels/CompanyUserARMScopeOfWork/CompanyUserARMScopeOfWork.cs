using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserARMScopeOfWork : DomainModelBridge
    {
        protected readonly ILogger<CompanyUserARMScopeOfWork> _logger;

        protected CompanyUserARMScopeOfWork()
        {
        }

        public CompanyUserARMScopeOfWork(ILogger<CompanyUserARMScopeOfWork> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Product { get; set; }

        [StringLength(50)]
        public string? ProductGroup { get; set; }

        [StringLength(50)]
        public string? SubProduct { get; set; }

        [StringLength(50)]
        public string? Bucket { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? Region { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? Zone { get; set; }

        [StringLength(50)]
        public string? Branch { get; set; }

        [StringLength(32)]
        public string? SupervisingManagerId { get; set; }

        public CompanyUser SupervisingManager { get; set; }

        public Agency ReportingAgency { get; set; }

        [StringLength(32)]
        public string? ReportingAgencyId { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(32)]
        public string? CompanyUserId { get; set; }

        public CompanyUser CompanyUser { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}