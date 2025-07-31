using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentByRule : DomainModelBridge
    {
        protected readonly ILogger<TreatmentByRule> _logger;

        public TreatmentByRule()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentByRule>>();
        }

        public TreatmentByRule(ILogger<TreatmentByRule> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? TreatmentId { get; set; }

        public Treatment? Treatment { get; set; }

        [StringLength(32)]
        public string? SubTreatmentId { get; set; }

        public SubTreatment? SubTreatment { get; set; }

        [StringLength(32)]
        public string? DepartmentId { get; set; }

        public Department? Department { get; set; }

        [StringLength(32)]
        public string? DesignationId { get; set; }

        public Designation? designation { get; set; }

        [StringLength(50)]
        public string? Rule { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}