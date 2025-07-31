namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetExecutionHistoryDetailsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? TreatmentId { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public string? NoOfAccounts { get; set; }
    }
}