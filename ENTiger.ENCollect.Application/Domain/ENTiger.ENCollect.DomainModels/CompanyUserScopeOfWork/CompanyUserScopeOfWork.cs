using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUserScopeOfWork : DomainModelBridge
    {
        protected readonly ILogger<CompanyUserScopeOfWork> _logger;

        protected CompanyUserScopeOfWork()
        {
        }

        public CompanyUserScopeOfWork(ILogger<CompanyUserScopeOfWork> logger)
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

        [StringLength(100)]
        public string? Location { get; set; }

        public CompanyUser SupervisingManager { get; set; }

        [StringLength(32)]
        public string? SupervisingManagerId { get; set; }

        public Department Department { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        public Designation Designation { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }

        [ForeignKey("CompanyUserId")]
        public CompanyUser CompanyUser { get; set; }

        [StringLength(32)]
        public string? CompanyUserId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}