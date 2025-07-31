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
    [Table("CollectionBatches")]
    public partial class CollectionBatch : DomainModelBridge
    {
        protected readonly ILogger<CollectionBatch> _logger;
        protected readonly IFlexHost _flexHost;

        public CollectionBatch()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
        }

        public CollectionBatch(ILogger<CollectionBatch> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        #region "Attributes"

        #region "Public"

        [StringLength(100)]
        public string? CustomId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }

        [StringLength(100)]
        public string? ProductGroup { get; set; }

        [StringLength(5)]
        public string? CurrencyId { get; set; }

        public string? ModeOfPayment { get; set; }

        [StringLength(50)]
        public string? BankAccountNo { get; set; }

        [StringLength(50)]
        public string? BankName { get; set; }

        [StringLength(50)]
        public string? BranchName { get; set; }

        [StringLength(50)]
        public string? AccountHolderName { get; set; }

        [StringLength(50)]
        public string? Latitude { get; set; }

        [StringLength(50)]
        public string? Longitude { get; set; }

        [StringLength(32)]
        public string? CollectionBatchOrgId { get; set; }

        public ApplicationOrg CollectionBatchOrg { get; set; }

        public CompanyUser AcknowledgedBy { get; set; }

        [StringLength(32)]
        public string? AcknowledgedById { get; set; }

        [StringLength(32)]
        public string? PayInSlipId { get; set; }

        public PayInSlip PayInSlip { get; set; }

        public ICollection<Collection> Collections { get; set; }
        public CollectionBatchWorkflowState CollectionBatchWorkflowState { get; set; }

        [StringLength(32)]
        public string? CollectionBatchWorkflowStateId { get; set; }

        public string? BatchType { get; set; }

        #endregion "Public"

        #endregion "Attributes"

        #region "Public Methods"

        public void SetAmount(decimal? amount)
        {
            Amount = amount;
        }

        public void SetCurrencyId(string currencyId)
        {
            CurrencyId = currencyId;
        }

        public void SetModeOfPayment(string modeOfPayment)
        {
            ModeOfPayment = modeOfPayment;
        }

        public void SetPayInSlipId(string payInSlipId)
        {
            PayInSlipId = payInSlipId;
        }

        public void AddCollectionsToBatch(ICollection<Collection> collections)
        {
            Collections = collections;
        }

        public void SetUpdatedCollectionState(int removedCollectionCount, StateChange stateChange)
        {
            int num = 0;
            num = Collections.Count();
            if (num == removedCollectionCount)
            {
                MarkAsDissolved(stateChange);
            }
        }

        public void MarkAsCreated(string userid)
        {
            CollectionBatchWorkflowState = _flexHost.GetFlexStateInstance<CollectionBatchCreated>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        public void MarkAsCreatedForPartner(StateChange stateChange)
        {
            //CollectionBatchWorkflowState = FlexOpus.GetFlexStateInstance<CollectionBatchCreatedForPartner>(stateChange);
        }

        public void MarkAsDissolved(StateChange stateChange)
        {
            //CollectionBatchWorkflowState = FlexOpus.GetFlexStateInstance<Dissolved>(stateChange);
        }

        public void MarkAsAcknowledged(string userid)
        {
            CollectionBatchWorkflowState = _flexHost.GetFlexStateInstance<CollectionBatchAcknowledged>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        public void MarkAsAcknowledgedByPartner(string userid)
        {
            //CollectionBatchWorkflowState = FlexOpus.GetFlexStateInstance<CollectionBatchAcknowledgedByPartner>(stateChange);
            CollectionBatchWorkflowState = _flexHost.GetFlexStateInstance<CollectionBatchAcknowledgedByPartner>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        public void MarkAsAddedinPayInSlip(StateChange stateChange)
        {
            //CollectionBatchWorkflowState = FlexOpus.GetFlexStateInstance<AddedCollectionBatchInPayInSlip>(stateChange);
        }

        public void SetUpdatedCollectionState(int removedCollectionCount, string userid)
        {
            int batchCollectionCount = 0;
            StateChange sc = new StateChange(userid);

            batchCollectionCount = this.Collections.Count();

            if (batchCollectionCount == removedCollectionCount)
            {
                this.MarkAsDissolved(sc);
            }
        }

        #endregion "Public Methods"
    }
}