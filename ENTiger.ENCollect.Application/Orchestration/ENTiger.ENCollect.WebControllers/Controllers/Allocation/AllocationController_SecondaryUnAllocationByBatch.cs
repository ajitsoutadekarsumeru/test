using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("account/unallocation/secondary/batch")]
        [Authorize(Policy = "CanUploadSecondaryDeAllocationBatchPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SecondaryUnAllocationByBatch([FromBody] SecondaryUnAllocationByBatchDto dto)
        {
            var result = RateLimit(dto, "upload_secondary_unallocation");
            return result ?? await RunService(201, dto, _processAllocationService.SecondaryUnAllocationByBatch);
        }
    }
}