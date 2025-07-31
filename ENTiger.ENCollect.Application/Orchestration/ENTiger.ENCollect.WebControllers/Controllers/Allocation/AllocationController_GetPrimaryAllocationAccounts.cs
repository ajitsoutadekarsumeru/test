using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("accounts/allocations/primary/download")]
        [ProducesResponseType(typeof(IEnumerable<GetPrimaryAllocationAccountsDto>), 200)]
        public async Task<IActionResult> GetPrimaryAllocationAccounts([FromBody] GetPrimaryAllocationAccountsParams parameters)
        {
            return await RunQueryListServiceAsync<GetPrimaryAllocationAccountsParams, GetPrimaryAllocationAccountsDto>(
                        parameters, _processAllocationService.GetPrimaryAllocationAccounts);
        }
    }
}