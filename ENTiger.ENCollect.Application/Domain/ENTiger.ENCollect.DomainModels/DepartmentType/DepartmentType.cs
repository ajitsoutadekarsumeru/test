using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DepartmentType : DomainModelBridge
    {
        protected readonly ILogger<DepartmentType> _logger;

        protected DepartmentType()
        {
        }

        public DepartmentType(ILogger<DepartmentType> logger)
        {
            _logger = logger;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(50)]
        public string? Name { get; set; }

        #endregion "Protected"

        #endregion "Attributes"
    }
}