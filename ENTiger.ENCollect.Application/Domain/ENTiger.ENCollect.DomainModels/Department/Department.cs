using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    public partial class Department : DomainModelBridge
    {
        protected readonly ILogger<Department> _logger;

        protected Department()
        {
        }

        public Department(ILogger<Department> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? Name { get; set; }

        [StringLength(50)]
        public string? Acronym { get; set; }

        [StringLength(50)]
        public string? Code { get; set; }

        public DepartmentType? DepartmentType { get; set; }

        [StringLength(32)]
        public string? DepartmentTypeId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}