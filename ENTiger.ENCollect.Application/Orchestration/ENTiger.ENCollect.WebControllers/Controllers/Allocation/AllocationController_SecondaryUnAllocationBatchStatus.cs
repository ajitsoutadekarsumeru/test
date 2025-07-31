using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost()]
        [Route("account/unallocation/secondary/batch/status")]
        [Authorize(Policy = "CanSearchSecondaryDeAllocationBatchStatusPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SecondaryUnAllocationBatchStatusDto>), 200)]
        public async Task<IActionResult> SecondaryUnAllocationBatchStatus([FromBody] SecondaryUnAllocationBatchStatusParams parameters)
        {
            return await RunQueryPagedServiceAsync<SecondaryUnAllocationBatchStatusParams, SecondaryUnAllocationBatchStatusDto>(parameters, _processAllocationService.SecondaryUnAllocationBatchStatus);
        }
    }
}