using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("batch/allocations/secondary/batch")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SearchAccountsForSecondaryAllocation([FromBody] SearchAccountsForSecondaryAllocationDto dto)
        {
            string customid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            dto.customId = customid;
            var result = RateLimit(dto, "download_secondary_allocation");
            return result ?? await RunService(201, dto, _processAllocationService.SearchAccountsForSecondaryAllocation);
        }
    }
}