using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentUpdateIntermediate : DomainModelBridge
    {
        protected readonly ILogger<TreatmentUpdateIntermediate> _logger;

        protected TreatmentUpdateIntermediate()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentUpdateIntermediate>>();
        }

        public TreatmentUpdateIntermediate(ILogger<TreatmentUpdateIntermediate> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(50)]
        public string? AgreementId { get; set; }

        [StringLength(50)]
        public string? AllocationOwnerId { get; set; }

        [StringLength(50)]
        public string? TCAgencyId { get; set; }

        [StringLength(50)]
        public string? AgencyId { get; set; }

        [StringLength(50)]
        public string? TellecallerId { get; set; }

        [StringLength(50)]
        public string? CollectorId { get; set; }

        [StringLength(50)]
        public string? TreatmentId { get; set; }

        [StringLength(50)]
        public string? WorkRequestId { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}