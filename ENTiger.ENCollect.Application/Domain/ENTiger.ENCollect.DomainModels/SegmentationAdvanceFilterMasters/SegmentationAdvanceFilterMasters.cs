using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SegmentationAdvanceFilterMasters : DomainModelBridge
    {
        protected readonly ILogger<SegmentationAdvanceFilterMasters> _logger;

        protected SegmentationAdvanceFilterMasters()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<SegmentationAdvanceFilterMasters>>();
        }

        public SegmentationAdvanceFilterMasters(ILogger<SegmentationAdvanceFilterMasters> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(200)]
        public string? FieldName { get; set; }

        [StringLength(200)]
        public string? FieldId { get; set; }

        [StringLength(100)]
        public string? Operator { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}