using Sumeru.Flex;

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
    public async Task<FlexiPagedList<GetSecondaryAllocationDetailsDto>> GetSecondaryAllocationDetails(GetSecondaryAllocationDetailsParams @params)
    {
        return await _flexHost.GetFlexiQuery<GetSecondaryAllocationDetails>().AssignParameters(@params).Fetch();
    }
}
