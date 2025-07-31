namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetSettlementByIdDto : DtoBridge
    {
        public string Id { get; set; }
        public string CustomId { get; set; }
        public string LoanAccountId { get; set; }
        public string Status { get; set; }
        public string CurrentBucket { get; set; }
        public decimal PrincipalOutstanding { get; set; }
        public int NumberOfEmisDue { get; set; }
        public decimal ChargesOutstanding { get; set; }
        public decimal InterestOutstanding { get; set; }
        public int NumberOfInstallments { get; set; }
        public DateTime SettlementDateForDuesCalc { get; set; }
        public string SettlementRemarks { get; set; }
        public string TrancheType { get; set; }
        public bool IsDeathSettlement { get; set; }
        public DateTimeOffset? DateOfDeath { get; set; }
        public decimal SettlementAmount { get; set; }
        public string CreatedBy { get; set; }

        public string WorkflowName { get; set; }
        public string WorkflowInstanceId { get; set; }
        public string StepName { get; set; }
        public string StepType { get; set; }
    }
}
