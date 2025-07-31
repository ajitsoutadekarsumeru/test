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
    [Table("Receipts")]
    public partial class Receipt : DomainModelBridge
    {
        protected readonly ILogger<Receipt> _logger;
        protected readonly IFlexHost _flexHost;

        public Receipt()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
        }

        public Receipt(ILogger<Receipt> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(32)]
        public string CollectorId { get; set; }

        public ApplicationUser Collector { get; set; }
        public string? CustomId { get; set; }
        public ReceiptWorkflowState ReceiptWorkflowState { get; set; }

        #endregion "Protected"

        #endregion "Attributes"

        #region "Public Methods"

        public void MarkAsCollectionCollectedByCollector(string userid)
        {
            this.ReceiptWorkflowState = _flexHost.GetFlexStateInstance<CollectionCollectedByCollector>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        public void MarkAsAllocatedToCollector(string userid)
        {
            this.ReceiptWorkflowState = _flexHost.GetFlexStateInstance<ReceiptAllocatedToCollector>().SetTFlexId(this.Id).SetStateChangedBy(userid ?? "");
        }

        #endregion "Public Methods"
    }
}