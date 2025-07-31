namespace ENTiger.ENCollect.AllocationModule
{
    public class PrimaryAllocationByFilterCommand : FlexCommandBridge<PrimaryAllocationByFilterDto, FlexAppContextBridge>
    {
    }

    public class AgencyAllocationByFilterEventModel
    {
        public string? LoggedInUserId { get; set; }

        public string? AgencyId { get; set; }

        public string? TcAgencyId { get; set; }

        public string? AccountNo { get; set; }

        public DateTime? AgencyAllocationExpiryDate { get; set; }
    }
}