namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPrimaryAllocationAccountsDto : DtoBridge
    {
        public string? AccountNumber { get; set; }
        public string? CustomerNumber { get; set; }
        public string? SchemeCode { get; set; }
        public string? SubProductName { get; set; }
        public string? CommunicationCityCode { get; set; }
        public string? Alpha { get; set; }
        public string? Region { get; set; }
        public string? Zone { get; set; }
        public string? TotalOverdue { get; set; }
        public string? NPAFlag { get; set; }
        public string? AllocationOwnerName { get; set; }
        public string? TCallingAgencyName { get; set; }
        public string? TCallingAgentName { get; set; }
        public string? AgencyName { get; set; }
        public string? AgentName { get; set; }
    }
}