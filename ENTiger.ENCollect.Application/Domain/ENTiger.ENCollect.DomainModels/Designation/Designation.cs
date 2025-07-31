using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class Designation : DomainModelBridge
    {
        protected readonly ILogger<Designation> _logger;

        public Designation()
        {
        }

        public Designation(ILogger<Designation> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Acronym { get; set; }

        public DesignationType? DesignationType { get; set; }

        [StringLength(32)]
        public string? DesignationTypeId { get; set; }

        public Department? Department { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        [StringLength(10)]
        public int Level { get; set; }

        [StringLength(200)]
        public string? ReportsToDesignation { get; set; }
        [StringLength(32)]
        public string? PermissionSchemeId { get; set; }

        public PermissionSchemes? PermissionScheme { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        #region "Private Methods"

        public Designation disableDesignation(Designation designation, string userId)
        {
            this.IsDeleted = true;
            this.LastModifiedBy = userId;
            return designation;
        }

        #endregion "Private Methods"
    }
}