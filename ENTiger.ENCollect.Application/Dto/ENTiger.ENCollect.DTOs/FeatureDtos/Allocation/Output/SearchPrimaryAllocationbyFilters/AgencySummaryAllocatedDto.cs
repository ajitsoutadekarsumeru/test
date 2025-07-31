namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencySummaryAllocatedDto : DtoBridge
    {
        public string TelleCallerAgencyName { get; set; }

        public string AgencyName { get; set; }

        public string Count { get; set; }

        public decimal? TOS { get; set; }

        public decimal? Tospercentage { get; set; }
    }
}