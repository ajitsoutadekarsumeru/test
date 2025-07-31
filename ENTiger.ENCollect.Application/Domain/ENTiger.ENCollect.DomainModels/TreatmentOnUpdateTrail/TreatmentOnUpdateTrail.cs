using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnUpdateTrail : DomainModelBridge
    {
        protected readonly ILogger<TreatmentOnUpdateTrail> _logger;

        protected TreatmentOnUpdateTrail()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentOnUpdateTrail>>();
        }

        public TreatmentOnUpdateTrail(ILogger<TreatmentOnUpdateTrail> logger)
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

        [StringLength(100)]
        public string? DispositionCodeGroup { get; set; }

        [StringLength(100)]
        public string? DispositionCode { get; set; }

        public DateTime? NextActionDate { get; set; }
        public decimal? PTPAmount { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}