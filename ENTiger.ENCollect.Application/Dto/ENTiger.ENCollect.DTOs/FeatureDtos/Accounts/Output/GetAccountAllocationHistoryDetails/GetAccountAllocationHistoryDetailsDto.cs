namespace ENTiger.ENCollect.AccountsModule;

/// <summary>
///
/// </summary>
public partial class GetAccountAllocationHistoryDetailsDto : DtoBridge
{
    //public int Count { get; set; }
    //public List<GetAccountAllocationHistoryDetailsModel> output { get; set; }

    public string? Id { get; set; }

    public string? AccountNo { get; set; }
    public string? ExecutionTime { get; set; }

    public string? TransactionIdOrName { get; set; }

    public string? AllocatedByUserCode { get; set; }
    public string? AllocatedByUserName { get; set; }

    public string? TypeOfAllocation { get; set; }

    public string? MethodOfAllocation { get; set; }

    public string? TCAgencyName { get; set; }

    public string? TCAgencyCode { get; set; }

    public string? TCAgentName { get; set; }

    public string? TCAgentCode { get; set; }

    public string? FieldAgencyName { get; set; }

    public string? FieldAgencyCode { get; set; }

    public string? FieldAgentOrStaffName { get; set; }

    public string? FieldAgentOrStaffCode { get; set; }

    public string? FieldAgencyAllocationExpiryDate { get; set; }

    public string? FieldAgentOrStaffAllocationExpiryDate { get; set; }

    public string? AllocationOwnerName { get; set; }

    public string? AllocationOwnerCode { get; set; }

    public string? CreatedDate { get; set; }
}

public class GetAccountAllocationHistoryDetailsModel
{
}