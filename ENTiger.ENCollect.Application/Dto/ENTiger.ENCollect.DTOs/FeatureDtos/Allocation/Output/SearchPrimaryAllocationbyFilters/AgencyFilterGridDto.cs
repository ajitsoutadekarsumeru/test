namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyFilterGridDto : DtoBridge
    {
        public string Id { get; set; }

        public string AccountNo { get; set; }

        public string CustomerName { get; set; }

        public string Product { get; set; }

        public string Bucket { get; set; }

        public decimal? Tos { get; set; }

        public string delstring { get; set; }

        public string dpd { get; set; }

        public string TCAgencyName { get; set; }

        public string AgencyName { get; set; }

        public string TCAgentName { get; set; }

        public string AgentName { get; set; }
        public string? CUSTOMERID { get; set; }
    }
}