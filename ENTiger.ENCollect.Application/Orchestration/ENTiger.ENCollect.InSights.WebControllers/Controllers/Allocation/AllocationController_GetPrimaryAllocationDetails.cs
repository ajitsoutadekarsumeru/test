namespace ENTiger.ENCollect.AllocationModule;


public partial class AllocationController : FlexControllerBridge<AllocationController>
{
    [HttpPost]
    [Route("primary/allocation/details")]
    [ProducesResponseType(typeof(FlexiPagedList<GetPrimaryAllocationDetailsDto>), 200)]
    public async Task<IActionResult> GetPrimaryAllocationDetails([FromBody]GetPrimaryAllocationDetailsParams parameters)
    {
        return await RunQueryPagedServiceAsync<GetPrimaryAllocationDetailsParams, GetPrimaryAllocationDetailsDto>(parameters, _processAllocationService.GetPrimaryAllocationDetails);
    }
}    
