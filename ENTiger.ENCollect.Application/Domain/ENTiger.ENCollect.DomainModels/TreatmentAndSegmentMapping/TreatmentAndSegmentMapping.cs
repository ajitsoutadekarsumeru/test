using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentAndSegmentMapping : DomainModelBridge
    {
        protected readonly ILogger<TreatmentAndSegmentMapping> _logger;

        protected TreatmentAndSegmentMapping()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentAndSegmentMapping>>();
        }

        public TreatmentAndSegmentMapping(ILogger<TreatmentAndSegmentMapping> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? TreatmentId { get; set; }

        public Treatment? Treatment { get; set; }

        [StringLength(32)]
        public string? SegmentId { get; set; }

        public Segmentation? Segment { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}