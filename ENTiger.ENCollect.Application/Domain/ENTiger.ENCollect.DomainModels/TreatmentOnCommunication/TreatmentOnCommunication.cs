using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnCommunication : DomainModelBridge
    {
        protected readonly ILogger<TreatmentOnCommunication> _logger;

        protected TreatmentOnCommunication()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentOnCommunication>>();
        }

        public TreatmentOnCommunication(ILogger<TreatmentOnCommunication> logger)
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
        public string? CommunicationType { get; set; }

        [StringLength(32)]
        public string? CommunicationTemplateId { get; set; }

        public CommunicationTemplate? CommunicationTemplate { get; set; }

        [StringLength(200)]
        public string? CommunicationMobileNumberType { get; set; }

        #endregion "Public"

        #endregion "Attributes"
    }
}