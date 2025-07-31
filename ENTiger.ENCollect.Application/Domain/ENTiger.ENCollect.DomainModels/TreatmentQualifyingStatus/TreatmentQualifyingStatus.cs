using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentQualifyingStatus : DomainModelBridge
    {
        protected readonly ILogger<TreatmentQualifyingStatus> _logger;

        protected TreatmentQualifyingStatus()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentQualifyingStatus>>();
        }

        public TreatmentQualifyingStatus(ILogger<TreatmentQualifyingStatus> logger)
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

        [StringLength(200)]
        public string? Status { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}