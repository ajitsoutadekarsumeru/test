namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchSecondaryAllocationbyFiltersDto : DtoBridge
    {
        public Int64 count { get; set; }
        public decimal? totalTos { get; set; }
        public ICollection<AgentSummaryUnAllocatedOutputApiModel> UnAllocated { get; set; }
        public ICollection<AgentSummaryAllocatedOutPutApiModel> Allocated { get; set; }
        public ICollection<AgencyFilterGridOutPutApiModel> AccountList { get; set; }
    }

    public class AgentSummaryUnAllocatedOutputApiModel
    {
        public string? TeleCallerAgencyName { get; set; }
        public string? AgencyName { get; set; }
        public string? Count { get; set; }
        public decimal? Tos { get; set; }
        public decimal? TosPercentage { get; set; }
    }

    public class AgentSummaryAllocatedOutPutApiModel
    {
        public string? TeleCallerAgentName { get; set; }
        public string? AgentName { get; set; }
        public string? Count { get; set; }
        public decimal? TOS { get; set; }
        public decimal? Tospercentage { get; set; }
    }

    public class AgencyFilterGridOutPutApiModel
    {
        public string? Id { get; set; }
        public string? AccountNo { get; set; }
        public string? CustomerName { get; set; }
        public string? Product { get; set; }

        //public long? Bucket { get; set; }
        public string? Bucket { get; set; }

        public decimal? Tos { get; set; }
        public string? delstring { get; set; }
        public string? dpd { get; set; }
        public string? TCAgencyName { get; set; }
        public string? AgencyName { get; set; }
        public string? TCAgentName { get; set; }
        public string? AgentName { get; set; }
        public string? CUSTOMERID { get; set; }
    }

    public class SearchSecondaryAllocationbyLoanAccountsDto
    {
        public string? Id { get; set; }

        public string? CITY { get; set; }

        public string? CustomId { get; set; }

        public string? CUSTOMERNAME { get; set; }

        public string? PRODUCT { get; set; }

        public string? ProductGroup { get; set; }

        public long? BUCKET { get; set; }

        public long? CURRENT_DPD { get; set; }

        public string? TOS { get; set; }

        public string? AgencyId { get; set; }

        public string? TeleCallingAgencyId { get; set; }

        public string? CollectorId { get; set; }

        public string? TeleCallerId { get; set; }

        public string? CollectorName { get; set; }

        public string? TeleCallerName { get; set; }

        public string? AgencyName { get; set; }

        public string? TeleCallingAgencyName { get; set; }

        public string? AllocationOwnerId { get; set; }
        public string? CUSTOMERID { get; set; }
    }
}