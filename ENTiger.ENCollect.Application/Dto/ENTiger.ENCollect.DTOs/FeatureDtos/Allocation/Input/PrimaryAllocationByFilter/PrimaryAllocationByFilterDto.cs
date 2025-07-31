namespace ENTiger.ENCollect.AllocationModule
{
    public partial class PrimaryAllocationByFilterDto : DtoBridge
    {
        public bool? AllocateByTos { get; set; }
        public bool? AllocateToAgency { get; set; }
        public bool? AllocateByCount { get; set; }

        public string? TelecallingAgencyId { get; set; }
        public string? AgencyId { get; set; }
        public DateTime? allocationExpireDate { get; set; }
        public List<string> AccountIds { get; set; }
        public Int64 percentageTos { get; set; }
        public Int64 AccountCount { get; set; }

        public string? AgentId { get; set; }
        public string? TelecallerAgentId { get; set; }
    }
}