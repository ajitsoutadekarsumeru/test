namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetTrailGapDetailsDto : DtoBridge
    {
        public string? FieldAgencyId { get; set; }

        public string? FieldAgencyName { get; set; }

        public string? FieldAgentId { get; set; }

        public string? FieldAgentName { get; set; }

        public string? ProductGroup { get; set; }

        public string? Product { get; set; }

        public string? SubProduct { get; set; }

        public string? CurrentBucket { get; set; }

        public string? Bucket { get; set; }

        public string? Region { get; set; }

        public string? State { get; set; }

        public string? City { get; set; }

        public string? Branch { get; set; }

        public string? AccountNo { get; set; }

        public string? Status { get; set; }

        public string? TrailGroup { get; set; }

        public decimal? TotalOutstandingAmount { get; set; }

    }

}   