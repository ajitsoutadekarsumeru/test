using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnCommunicationHistoryDetails : DomainModelBridge
    {
        protected readonly ILogger<TreatmentOnCommunicationHistoryDetails> _logger;

        protected TreatmentOnCommunicationHistoryDetails()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<TreatmentOnCommunicationHistoryDetails>>();
        }

        public TreatmentOnCommunicationHistoryDetails(ILogger<TreatmentOnCommunicationHistoryDetails> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(32)]
        public string? TreatmentHistoryId { get; set; }

        public TreatmentHistory? TreatmentHistory { get; set; }
        public string? LoanAccountId { get; set; }
        public LoanAccount? LoanAccount { get; set; }

        [StringLength(50)]
        public string? CustomId { get; set; }

        [StringLength(800)]
        public string? ReasonForFailure { get; set; }

        public DateTime? ReturnDate { get; set; }
        public string? ReasonForReturn { get; set; }

        [StringLength(200)]
        public string? DispatchID { get; set; }

        public DateTime? DispatchDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        [StringLength(100)]
        public string? Status { get; set; }

        [StringLength(8000)]
        public string? MessageContent { get; set; }

        [StringLength(2000)]
        public string? WAapiResponse { get; set; }

        [StringLength(100)]
        public string? WADeliveredStatus { get; set; }

        [StringLength(800)]
        public string? WADeliveredResponse { get; set; }

        [StringLength(800)]
        public string? FileName { get; set; }

        [StringLength(32)]
        public string? SubTreatmentId { get; set; }

        public SubTreatment? SubTreatment { get; set; }

        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }

        public bool IsDelivered { get; set; }

        public string? SMSContentCreated { get; set; }

        public DateTime? SMSContentCreatedTimeStamp { get; set; }

        public string? SMSContentRequest { get; set; }

        public DateTime? SMSContentRequestTimeStamp { get; set; }

        public string? SMSResponse { get; set; }

        public DateTime? SMSResponseTimeStamp { get; set; }

        [StringLength(50)]
        public string? SMSResponseStatus { get; set; }

        public string? EmailContentCreated { get; set; }

        public DateTime? EmailContentCreatedTimeStamp { get; set; }

        public string? EmailContentRequest { get; set; }

        public DateTime? EmailContentRequestTimeStamp { get; set; }

        public string? EmailResponse { get; set; }

        public DateTime? EmailResponseTimeStamp { get; set; }

        [StringLength(50)]
        public string? EmailResponseStatus { get; set; }

        public string? WAContentCreated { get; set; }

        public DateTime? WAContentCreatedTimeStamp { get; set; }

        public string? WAContentRequest { get; set; }

        public DateTime? WAContentRequestTimeStamp { get; set; }

        public string? WAResponse { get; set; }

        public DateTime? WAResponseTimeStamp { get; set; }

        [StringLength(50)]
        public string? WAResponseStatus { get; set; }

        [StringLength(32)]
        public string? CommunicationTemplateId { get; set; }

        public CommunicationTemplate? CommunicationTemplate { get; set; }

        [StringLength(32)]
        public string? PaymentTransactionId { get; set; }

        public PaymentTransaction? PaymentTransaction { get; set; }

        [StringLength(200)]
        public string? SMS_Aggregator_TransactionID { get; set; }

        [StringLength(200)]
        public string? WA_Aggregator_TransactionID { get; set; }

        [StringLength(200)]
        public string? Recipient_Operator { get; set; }

        [StringLength(200)]
        public string? Recipient_Circle { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateOnly? DeliveryDate_Only { get; set; }
        #endregion "Public"

        #endregion "Attribute"


    }
}