namespace ENTiger.ENCollect
{
    public class GetLoanAccountsOutputDto : DtoBridge
    {
        public string AccountNumber { get; set; }
        public string CustomerNumber { get; set; }
        public string SchemeCode { get; set; }
        public string SubProductName { get; set; }
        public string CommunicationCityCode { get; set; }
        public string Branch { get; set; }
        public string Region { get; set; }
        public string Zone { get; set; }
        public string TotalOverdue { get; set; }
        public string NPAFlag { get; set; }

        public string State { get; set; }

        public string BOM_POS { get; set; }
        public string AllocationOwnerName { get; set; }
        public string TCallingAgencyName { get; set; }
        public string TCallingAgentName { get; set; }
        public string AgencyName { get; set; }
        public string AgentName { get; set; }
        public string SegmentName { get; set; }
        public string TreatmentName { get; set; }
    }
}