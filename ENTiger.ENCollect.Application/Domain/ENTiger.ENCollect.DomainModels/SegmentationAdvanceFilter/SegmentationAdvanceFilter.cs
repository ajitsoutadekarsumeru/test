using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SegmentationAdvanceFilter : DomainModelBridge
    {
        protected readonly ILogger<SegmentationAdvanceFilter> _logger;

        protected SegmentationAdvanceFilter()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<SegmentationAdvanceFilter>>();
        }

        public SegmentationAdvanceFilter(ILogger<SegmentationAdvanceFilter> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        [StringLength(50)]
        public string? BOM_POS { get; set; }

        [StringLength(50)]
        public string? CHARGEOFF_DATE { get; set; }

        [StringLength(50)]
        public string? CURRENT_POS { get; set; }

        [StringLength(50)]
        public string? LOAN_AMOUNT { get; set; }

        [StringLength(50)]
        public string? NONSTARTER { get; set; }

        [StringLength(50)]
        public string? NPA_STAGEID { get; set; }

        [StringLength(50)]
        public string? PRINCIPAL_OD { get; set; }

        [StringLength(50)]
        public string? TOS { get; set; }

        [StringLength(50)]
        public string? Area { get; set; }

        [StringLength(250)]
        public string? LastDispositionCode { get; set; }

        [StringLength(50)]
        public string? LastPaymentDate { get; set; }

        [StringLength(200)]
        public string? DispCode { get; set; }

        [StringLength(50)]
        public string? PTPDate { get; set; }

        [StringLength(200)]
        public string? CustomerPersona { get; set; }

        [StringLength(50)]
        public string? CurrentDPD { get; set; }

        [StringLength(50)]
        public string? CreditBureauScore { get; set; }

        [StringLength(50)]
        public string? CustomerBehaviourScore1 { get; set; }

        [StringLength(50)]
        public string? CustomerBehaviourScore2 { get; set; }

        [StringLength(50)]
        public string? EarlyWarningScore { get; set; }

        [StringLength(50)]
        public string? LegalStage { get; set; }

        [StringLength(50)]
        public string? RepoStage { get; set; }

        [StringLength(50)]
        public string? SettlementStage { get; set; }

        [StringLength(50)]
        public string? CustomerBehaviorScoreToKeepHisWord { get; set; }

        [StringLength(50)]
        public string? PreferredModeOfPayment { get; set; }

        [StringLength(50)]
        public string? PropensityToPayOnline { get; set; }

        [StringLength(50)]
        public string? DigitalContactabilityScore { get; set; }

        [StringLength(50)]
        public string? CallContactabilityScore { get; set; }

        [StringLength(50)]
        public string? FieldContactabilityScore { get; set; }

        [StringLength(50)]
        public string? Latest_Status_Of_SMS { get; set; }

        [StringLength(50)]
        public string? Latest_Status_Of_WhatsUp { get; set; }

        [StringLength(50)]
        public string? StatementDate { get; set; }

        [StringLength(50)]
        public string? DueDate { get; set; }

        [StringLength(50)]
        public string? TotalOverdueAmount { get; set; }

        [StringLength(50)]
        public string? DNDFlag { get; set; }

        [StringLength(50)]
        public string? MinimumAmountDue { get; set; }

        [StringLength(50)]
        public string? Month { get; set; }

        [StringLength(50)]
        public string? Year { get; set; }

        [StringLength(50)]
        public string? LOAN_STATUS { get; set; }

        [StringLength(50)]
        public string? EMI_OD_AMT { get; set; }

        #endregion "Attributes"
    }
}