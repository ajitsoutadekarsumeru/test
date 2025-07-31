using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("secondary/agent/allocate")]
        [Authorize(Policy = "CanUpdateSecondaryAllocationByFilterPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SecondaryAllocationByFilter([FromBody] SecondaryAllocationByFilterDto dto)
        {
            var result = RateLimit(dto, "update_secondary_allocation");
            return result ?? await RunService(201, dto, _processAllocationService.SecondaryAllocationByFilter);
        }
    }
}