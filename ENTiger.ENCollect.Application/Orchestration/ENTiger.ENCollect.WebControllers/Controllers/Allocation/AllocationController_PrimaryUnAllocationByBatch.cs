using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("account/unallocation/primary/batch")]
        [Authorize(Policy = "CanUploadPrimaryDeAllocationBatchPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> PrimaryUnAllocationByBatch([FromBody] PrimaryUnAllocationByBatchDto dto)
        {
            var result = RateLimit(dto, "upload_primary_unallocation");
            return result ?? await RunService(201, dto, _processAllocationService.PrimaryUnAllocationByBatch);
        }
    }
}