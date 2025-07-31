namespace ENTiger.ENCollect.TreatmentModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUnSequencedTreatmentsDto : DtoBridge
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}