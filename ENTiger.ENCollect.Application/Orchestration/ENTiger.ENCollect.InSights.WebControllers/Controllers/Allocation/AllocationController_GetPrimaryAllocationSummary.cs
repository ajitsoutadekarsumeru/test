
namespace ENTiger.ENCollect.AllocationModule;

public partial class AllocationController : FlexControllerBridge<AllocationController>
{
    [HttpGet()]
    [Route("primary/allocation/summary")]
    [ProducesResponseType(typeof(IEnumerable<GetPrimaryAllocationSummaryDto>), 200)]
    public async Task<IActionResult> GetPrimaryAllocationSummary()
    {
        GetPrimaryAllocationSummaryParams parameters = new GetPrimaryAllocationSummaryParams();
        return await RunQueryListServiceAsync<GetPrimaryAllocationSummaryParams, GetPrimaryAllocationSummaryDto>(
                    parameters, _processAllocationService.GetPrimaryAllocationSummary);
    }
}
