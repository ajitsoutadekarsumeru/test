using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("batch/allocations/primary/batch")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SearchAccountsForPrimaryAllocation([FromBody] SearchAccountsForPrimaryAllocationDto dto)
        {
            string customid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            dto.customid = customid;
            var result = RateLimit(dto, "download_primary_allocation");
            return result ?? await RunService(201, dto, _processAllocationService.SearchAccountsForPrimaryAllocation);
        }
    }
}