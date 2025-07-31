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
    [Table("Collections")]
    public partial class Collection : DomainModelBridge
    {
        protected readonly ILogger<Collection> _logger;
        protected readonly IFlexHost _flexHost;

        protected Collection()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<Collection>>();
        }

        public Collection(ILogger<Collection> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string? CustomId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }

        [StringLength(5)]
        public string? CurrencyId { get; set; }

        public DateTime? CollectionDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? RecordNo { get; set; }

        [StringLength(50)]
        public string? CollectionMode { get; set; }

        [StringLength(50)]
        public string? MobileNo { get; set; }

        [StringLength(20)]
        public string? ContactType { get; set; }

        [StringLength(20)]
        public string? CountryCode { get; set; }

        [StringLength(20)]
        public string? AreaCode { get; set; }

        [StringLength(200)]
        public string? EMailId { get; set; }

        [StringLength(200)]
        public string? PayerImageName { get; set; }

        [StringLength(200)]
        public string? CustomerName { get; set; }

        [StringLength(200)]
        public string? ChangeRequestImageName { get; set; }

        [StringLength(50)]
        public string? PhysicalReceiptNumber { get; set; }

        [StringLength(50)]
        public string? Latitude { get; set; }

        [StringLength(50)]
        public string? Longitude { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }

        [StringLength(32)]
        public string? AccountId { get; set; }

        public LoanAccount Account { get; set; }

        [StringLength(32)]
        public string? CollectorId { get; set; }

        public ApplicationUser Collector { get; set; }

        [StringLength(32)]
        public string? AckingAgentId { get; set; }

        public ApplicationUser AckingAgent { get; set; }

        [StringLength(32)]
        public string? ReceiptId { get; set; }

        public Receipt Receipt { get; set; }

        [StringLength(32)]
        public string? CollectionOrgId { get; set; }

        public ApplicationOrg CollectionOrg { get; set; }

        [StringLength(32)]
        public string? CollectionBatchId { get; set; }

        public CollectionBatch CollectionBatch { get; set; }

        [StringLength(32)]
        public string? CashId { get; set; }

        public Cash Cash { get; set; }

        [StringLength(32)]
        public string? ChequeId { get; set; }

        public Cheque Cheque { get; set; }

        public int MailSentCount { get; set; }
        public int SMSSentCount { get; set; }

        [StringLength(32)]
        public string? TransactionNumber { get; set; }

        public DateTime? AcknowledgedDate { get; set; }

        public string? CollectionWorkflowStateId { get; set; }
        public CollectionWorkflowState CollectionWorkflowState { get; set; }

        [StringLength(32)]
        public string? CancelledCollectionId { get; set; }

        public Collection CancelledCollection { get; set; }

        [StringLength(500)]
        public string? CancellationRemarks { get; set; }

        public DateTime? OfflineCollectionDate { get; set; }

        [StringLength(500)]
        public string? GeoLocation { get; set; }

        [StringLength(500)]
        public string? EncredibleUserId { get; set; }

        [StringLength(50)]
        public string? yForeClosureAmount { get; set; }

        [StringLength(50)]
        public string? yOverdueAmount { get; set; }

        [StringLength(50)]
        public string? yBounceCharges { get; set; }

        [StringLength(50)]
        public string? othercharges { get; set; }

        [StringLength(50)]
        public string? yPenalInterest { get; set; }

        [StringLength(50)]
        public string? Settlement { get; set; }

        [StringLength(100)]
        public string? yRelationshipWithCustomer { get; set; }//

        [StringLength(200)]
        public string? yPANNo { get; set; }//

        [StringLength(50)]
        public string? yUploadSource { get; set; }//

        [StringLength(50)]
        public string? yBatchUploadID { get; set; }//

        public string? yTest { get; set; }//

        [StringLength(50)]
        public string? DepositAccountNumber { get; set; }

        [StringLength(50)]
        public string? DepositBankName { get; set; }

        [StringLength(50)]
        public string? DepositBankBranch { get; set; }

        public bool? IsPoolAccount { get; set; }
        public bool? IsDepositAccount { get; set; }
        public string? ReceiptType { get; set; }
        public bool? IsNewPhonenumber { get; set; }
        public string? ErrorMessgae { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? amountBreakUp1 { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? amountBreakUp2 { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? amountBreakUp3 { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? amountBreakUp4 { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? amountBreakUp5 { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal? amountBreakUp6 { get; set; }

        [StringLength(20)]
        public string? TransactionSource { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        public void SetCustomId(string customId)
        {
            this.CustomId = customId;
        }

        public void SetCollector(string collectorId)
        {
            this.CollectorId = collectorId;
        }

        public void SetAccount(string accountId)
        {
            this.AccountId = accountId;
        }

        public void SetAckingAgent(string ackingAgentId)
        {
            this.AckingAgentId = ackingAgentId;
        }

        public void SetAcknowledgeDate(DateTime ackDate)
        {
            this.AcknowledgedDate = ackDate;
        }

        public void SetCollectionOrg(string collectorId)
        {
        }

        public void MarkAsSuccess(string userid)
        {
            this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<CollectionSuccess>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");//FlexOpus.GetFlexStateInstance<CollectionSuccess>(stateChange);
        }

        public void MarkAsCancellationRequested(string userid)
        {
            StateChange stateChange = new StateChange(userid);

            this.LastModifiedBy = userid;
            this.LastModifiedDate = DateTime.Now;
            this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<CancellationRequested>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
            this.SetModified();
        }

        public void MarkAsCancellationRejected(string userid)
        {
            StateChange stateChange = new StateChange(userid);

            this.LastModifiedBy = userid;
            this.LastModifiedDate = DateTime.Now;
            this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<CancellationRejected>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
            this.SetModified();
        }

        public void MarkAsCancelled(string userid)
        {
            StateChange stateChange = new StateChange(userid);

            this.LastModifiedBy = userid;
            this.LastModifiedDate = DateTime.Now;
            this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<Cancelled>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
            this.SetModified();
        }

        public void MarkAsAddedInBatch(string userid)
        {
            this.CollectionWorkflowState = _flexHost.GetFlexStateInstance<AddedCollectionInBatch>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        public void MarkAsReceivedByCollector(string userid)
        {
            CollectionWorkflowState = _flexHost.GetFlexStateInstance<ReceivedByCollector>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        public void MarkAsReadyForBatch(string userid)
        {
            CollectionWorkflowState = _flexHost.GetFlexStateInstance<ReadyForBatch>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        public void SetInitialCollectionState(string userid)
        {
            if (CollectionMode != null &&
                (string.Equals(CollectionMode, CollectionModeEnum.Online.Value, StringComparison.OrdinalIgnoreCase) ||
                 string.Equals(CollectionMode, CollectionModeEnum.Card.Value, StringComparison.OrdinalIgnoreCase)))
            {
                CollectionWorkflowState = _flexHost.GetFlexStateInstance<CollectionInitiated>()
                    .SetTFlexId(this.Id)
                    .SetStateChangedBy(userid ?? "");
            }
            else
            {
                CollectionWorkflowState = _flexHost.GetFlexStateInstance<ReceivedByCollector>()
                    .SetTFlexId(this.Id)
                    .SetStateChangedBy(userid ?? "");
            }
        }


        public void MarkAsAcknowledged(string userid)
        {
            CollectionWorkflowState = _flexHost.GetFlexStateInstance<CollectionAcknowledged>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");//FlexOpus.GetFlexStateInstance<CollectionAcknowledged>(stateChange);
            CollectionWorkflowStateId = CollectionWorkflowState.Id;
        }
    }
}