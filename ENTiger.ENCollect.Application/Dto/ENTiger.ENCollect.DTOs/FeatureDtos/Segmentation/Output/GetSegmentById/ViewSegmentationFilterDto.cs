namespace ENTiger.ENCollect.DTOs.FeatureDtos.Segmentation.Output.GetSegmentById
{
    public class ViewSegmentationFilterDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? BOM_POS { get; set; }
        public string? CHARGEOFF_DATE { get; set; }
        public string? CURRENT_POS { get; set; }
        public string? LOAN_AMOUNT { get; set; }
        public string? NONSTARTER { get; set; }
        public string? NPA_STAGEID { get; set; }
        public string? PRINCIPAL_OD { get; set; }
        public string? TOS { get; set; }
        public string? Area { get; set; }
        public string? LastDispositionCode { get; set; }
        public string? LastPaymentDate { get; set; }
        public string? DispCode { get; set; }
        public string? PTPDate { get; set; }
        public string? CustomerPersona { get; set; }
        public string? CurrentDPD { get; set; }
        public string? CreditBureauScore { get; set; }
        public string? CustomerBehaviourScore1 { get; set; }
        public string? CustomerBehaviourScore2 { get; set; }
        public string? EarlyWarningScore { get; set; }
        public string? LegalStage { get; set; }
        public string? RepoStage { get; set; }
        public string? SettlementStage { get; set; }
        public string? CustomerBehaviorScoreToKeepHisWord { get; set; }
        public string? PreferredModeOfPayment { get; set; }
        public string? PropensityToPayOnline { get; set; }
        public string? DigitalContactabilityScore { get; set; }
        public string? CallContactabilityScore { get; set; }
        public string? FieldContactabilityScore { get; set; }
        public string? Latest_Status_Of_SMS { get; set; }
        public string? Latest_Status_Of_WhatsUp { get; set; }
        public string? StatementDate { get; set; }
        public string? DueDate { get; set; }
        public string? TotalOverdueAmount { get; set; }
        public string? DNDFlag { get; set; }
        public string? MinimumAmountDue { get; set; }
        public string? Month { get; set; }
        public string? Year { get; set; }
        public string? LOAN_STATUS { get; set; }
        public string? EMI_OD_AMT { get; set; }
    }
}