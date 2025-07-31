namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SecondaryAllocationByFilterDto : DtoBridge
    {
        public bool? AllocateByTos { get; set; }

        public bool? AllocateToAgents { get; set; }

        public bool? AllocateByCount { get; set; }

        public List<string> AccountIds { get; set; }

        public DateTime? allocationExpireDate1 { get; set; }

        public DateTime? allocationExpireDate2 { get; set; }

        public long percentageTos { get; set; }

        public long AccountCount { get; set; }

        public string? AgentId { get; set; }

        public string? TelecallerAgentId { get; set; }
    }
}