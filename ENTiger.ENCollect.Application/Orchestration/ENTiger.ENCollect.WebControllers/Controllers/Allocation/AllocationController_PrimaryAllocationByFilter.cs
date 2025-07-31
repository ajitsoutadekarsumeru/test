using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("primary/agency/allocate")]
        [Authorize(Policy = "CanUpdatePrimaryAllocationByFilterPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> PrimaryAllocationByFilter([FromBody] PrimaryAllocationByFilterDto dto)
        {
            var result = RateLimit(dto, "update_primary_allocation");
            return result ?? await RunService(201, dto, _processAllocationService.PrimaryAllocationByFilter);
        }
    }
}