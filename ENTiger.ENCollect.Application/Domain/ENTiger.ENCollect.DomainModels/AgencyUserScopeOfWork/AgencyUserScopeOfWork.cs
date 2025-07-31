using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUserScopeOfWork : DomainModelBridge
    {
        protected readonly ILogger<AgencyUserScopeOfWork> _logger;

        public AgencyUserScopeOfWork()
        {
        }

        public AgencyUserScopeOfWork(ILogger<AgencyUserScopeOfWork> logger)
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
        public string? DepartmentId { get; set; }

        public Department Department { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }

        public Designation Designation { get; set; }

        [StringLength(32)]
        public string? AgencyUserId { get; set; }

        public AgencyUser AgencyUser { get; set; }

        [StringLength(32)]
        public string? ManagerId { get; set; }

        [StringLength(100)]
        public string? Language { get; set; }

        public Int64 Experience { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}