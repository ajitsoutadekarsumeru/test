namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchPrimaryAllocationbyFiltersDto : DtoBridge
    {
        public long count { get; set; }

        public decimal? totalTos { get; set; }

        public ICollection<AgencySummaryUnAllocatedDto> UnAllocated { get; set; }

        public ICollection<AgencySummaryAllocatedDto> Allocated { get; set; }

        public ICollection<AgencyFilterGridDto> AccountList { get; set; }
    }

    public class SearchPrimaryAllocationbyLoanAccountsDto
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

        public string? AgencyName { get; set; }

        public string? TeleCallingAgencyName { get; set; }

        public string? AllocationOwnerId { get; set; }
        public string? CUSTOMERID { get; set; }
    }
}