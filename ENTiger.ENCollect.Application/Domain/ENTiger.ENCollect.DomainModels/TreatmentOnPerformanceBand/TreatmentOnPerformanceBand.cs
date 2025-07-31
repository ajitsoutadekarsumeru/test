using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnPerformanceBand : DomainModelBridge
    {
        protected readonly ILogger<TreatmentOnPerformanceBand> _logger;

        public TreatmentOnPerformanceBand()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentOnPerformanceBand>>();
        }

        public TreatmentOnPerformanceBand(ILogger<TreatmentOnPerformanceBand> logger)
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

        [StringLength(30)]
        public string? PerformanceBand { get; set; }

        [StringLength(200)]
        public string? CustomerPersona { get; set; }

        [StringLength(50)]
        public string? Percentage { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}