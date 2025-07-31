using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("account/unallocation/primary/batch/status")]
        [Authorize(Policy = "CanSearchPrimaryDeAllocationBatchStatusPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<PrimaryUnAllocationBatchStatusDto>), 200)]
        public async Task<IActionResult> PrimaryUnAllocationBatchStatus([FromBody] PrimaryUnAllocationBatchStatusParams parameters)
        {
            return await RunQueryPagedServiceAsync<PrimaryUnAllocationBatchStatusParams, PrimaryUnAllocationBatchStatusDto>(parameters, _processAllocationService.PrimaryUnAllocationBatchStatus);
        }
    }
}