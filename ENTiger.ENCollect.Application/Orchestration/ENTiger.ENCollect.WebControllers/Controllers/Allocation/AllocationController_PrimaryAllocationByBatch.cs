using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("accounts/allocations/agency/batch")]
        [Authorize(Policy = "CanUploadPrimaryAllocationBatchPolicy")]
        [Authorize(Policy = "CanUploadAllocationOwnerBatchPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> PrimaryAllocationByBatch([FromBody] PrimaryAllocationByBatchDto dto)
        {
            string customid = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            dto.Customid = customid;
            var result = RateLimit(dto, "upload_primary_allocation");
            return result ?? await RunService(201, dto, _processAllocationService.PrimaryAllocationByBatch);
        }
    }
}