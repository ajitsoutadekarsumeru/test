namespace ENTiger.ENCollect.AllocationModule;

/// <summary>
/// 
/// </summary>
public partial class ProcessAllocationService : ProcessFlexServiceBridge
{
    /// <summary>
    /// YourRemarksForMethod
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GetPrimaryAllocationSummaryDto>> GetPrimaryAllocationSummary(GetPrimaryAllocationSummaryParams @params)
    {
        return await  _flexHost.GetFlexiQuery<GetPrimaryAllocationSummary>().AssignParameters(@params).Fetch();
    }
}
