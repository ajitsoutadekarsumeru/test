namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencySummaryUnAllocatedDto : DtoBridge
    {
        public string city { get; set; }

        public string Count { get; set; }

        public decimal? Tos { get; set; }

        public decimal? TosPercentage { get; set; }
    }
}