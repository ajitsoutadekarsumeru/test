using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnPOS : DomainModelBridge
    {
        protected readonly ILogger<TreatmentOnPOS> _logger;

        public TreatmentOnPOS()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentOnPOS>>();
        }

        public TreatmentOnPOS(ILogger<TreatmentOnPOS> logger)
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
        public string? Percentage { get; set; }

        [StringLength(50)]
        public string? AllocationId { get; set; }

        [StringLength(100)]
        public string? AllocationName { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}