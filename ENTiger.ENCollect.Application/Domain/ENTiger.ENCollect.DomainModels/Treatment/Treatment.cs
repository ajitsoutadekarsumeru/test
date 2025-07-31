using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Treatment : DomainModelBridge
    {
        protected readonly ILogger<Treatment> _logger;

        protected Treatment()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<Treatment>>();
        }

        public Treatment(ILogger<Treatment> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(300)]
        public string? Name { get; set; }

        [StringLength(100)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string? Mode { get; set; }

        public bool? IsDisabled { get; set; }
        public int? Sequence { get; set; }

        [StringLength(50)]
        public string? PaymentStatusToStop { get; set; }

        public DateTime? ExecutionStartdate { get; set; }
        public DateTime? ExecutionEnddate { get; set; }

        public ICollection<TreatmentAndSegmentMapping>? segmentMapping { get; set; }
        public ICollection<SubTreatment>? subTreatment { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}