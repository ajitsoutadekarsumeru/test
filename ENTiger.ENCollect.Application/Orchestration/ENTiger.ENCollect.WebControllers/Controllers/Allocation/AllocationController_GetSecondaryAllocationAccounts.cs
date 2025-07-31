using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("accounts/allocations/secondary/download")]
        [ProducesResponseType(typeof(IEnumerable<GetSecondaryAllocationAccountsDto>), 200)]
        public async Task<IActionResult> GetSecondaryAllocationAccounts([FromBody] GetSecondaryAllocationAccountsParams parameters)
        {
            return await RunQueryListServiceAsync<GetSecondaryAllocationAccountsParams, GetSecondaryAllocationAccountsDto>(
                        parameters, _processAllocationService.GetSecondaryAllocationAccounts);
        }
    }
}