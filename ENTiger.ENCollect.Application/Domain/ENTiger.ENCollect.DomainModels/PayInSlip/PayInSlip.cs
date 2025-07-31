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
    [Table("PayInSlips")]
    public partial class PayInSlip : DomainModelBridge
    {
        protected readonly ILogger<PayInSlip> _logger;
        protected readonly IFlexHost _flexHost;

        protected PayInSlip()
        {
            _flexHost = FlexContainer.ServiceProvider.GetRequiredService<IFlexHost>();
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<PayInSlip>>();
        }

        public PayInSlip(ILogger<PayInSlip> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        #region "Attributes"



        #region "Protected"

        [StringLength(100)]
        public string CustomId { get; set; }

        [StringLength(100)]
        public string? CMSPayInSlipNo { get; set; }

        [StringLength(200)]
        public string? BankName { get; set; }

        [StringLength(200)]
        public string? BranchName { get; set; }

        public DateTime? DateOfDeposit { get; set; }

        [StringLength(50)]
        public string? BankAccountNo { get; set; }

        [StringLength(100)]
        public string? AccountHolderName { get; set; }

        [StringLength(100)]
        public string? ModeOfPayment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [StringLength(50)]
        public string? Currency { get; set; }

        [StringLength(50)]
        public bool? IsPrintValid { get; set; }

        public ApplicationUser PrintedBy { get; set; }

        [StringLength(32)]
        public string? PrintedById { get; set; }

        public DateTime? PrintedDate { get; set; }

        [StringLength(50)]
        public string? Lattitude { get; set; }

        [StringLength(50)]
        public string? Longitude { get; set; }

        [StringLength(250)]
        public string? PayInSlipImageName { get; set; }

        public string? PayinslipType { get; set; }
        public string? ProductGroup { get; set; }

        public List<CollectionBatch> CollectionBatches { get; set; }

        [StringLength(32)]
        public string? PayInSlipWorkflowStateId { get; set; }

        public PayInSlipWorkflowState PayInSlipWorkflowState { get; set; }


        [StringLength(20)]
        public string? TransactionSource { get; set; }


        #endregion "Protected"

        #endregion "Attributes"

        public void SetAmount(decimal amount)
        {
            Amount = amount;
        }

        public void SetCurrency(string currency)
        {
            Currency = currency;
        }

        public void SetModeOfPayment(string modeOfPayment)
        {
            ModeOfPayment = modeOfPayment;
        }

        public void SetCMSPayInSlipNo(string CMSPayInSlipNo)
        {
            this.CMSPayInSlipNo = CMSPayInSlipNo;
        }

        public void SetBankName(string bankName)
        {
            BankName = bankName;
        }

        public void SetBranchName(string branchName)
        {
            BranchName = branchName;
        }

        public void SetAccountHolderName(string accountHolderName)
        {
            AccountHolderName = accountHolderName;
        }

        public void SetBankAccountNo(string bankAccountNo)
        {
            BankAccountNo = bankAccountNo;
        }

        public void SetDateOfDeposit(DateTime? dateOfDeposit)
        {
            DateOfDeposit = dateOfDeposit;
        }
    }
}