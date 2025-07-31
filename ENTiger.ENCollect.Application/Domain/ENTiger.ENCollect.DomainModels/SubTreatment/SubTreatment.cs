using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SubTreatment : DomainModelBridge
    {
        protected readonly ILogger<SubTreatment> _logger;

        public SubTreatment()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<SubTreatment>>();
        }

        public SubTreatment(ILogger<SubTreatment> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        public int? Order { get; set; }

        [StringLength(100)]
        public string? TreatmentType { get; set; }

        [StringLength(100)]
        public string? AllocationType { get; set; }

        [StringLength(20)]
        public string? StartDay { get; set; }

        [StringLength(20)]
        public string? EndDay { get; set; }

        public string? ScriptToPersueCustomer { get; set; }

        [StringLength(20)]
        public string? QualifyingCondition { get; set; }

        public int? PreSubtreatmentOrder { get; set; }

        [StringLength(100)]
        public string? QualifyingStatus { get; set; }

        public ICollection<TreatmentOnPOS>? POSCriteria { get; set; }
        public ICollection<TreatmentOnAccount>? AccountCriteria { get; set; }
        public ICollection<RoundRobinTreatment>? RoundRobinCriteria { get; set; }
        public ICollection<TreatmentByRule>? TreatmentByRule { get; set; }
        public ICollection<TreatmentOnPerformanceBand>? PerformanceBand { get; set; }
        public ICollection<TreatmentDesignation>? Designation { get; set; }
        public TreatmentOnUpdateTrail? TreatmentOnUpdateTrail { get; set; }
        public TreatmentOnCommunication? TreatmentOnCommunication { get; set; }
        public ICollection<TreatmentQualifyingStatus>? DeliveryStatus { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}