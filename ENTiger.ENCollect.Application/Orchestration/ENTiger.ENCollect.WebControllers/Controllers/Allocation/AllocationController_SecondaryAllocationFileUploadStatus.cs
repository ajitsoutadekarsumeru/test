using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("accounts/allocations/collector/status")]
        [Authorize(Policy = "CanSearchSecondaryAllocationBatchStatusPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<SecondaryAllocationFileUploadStatusDto>), 200)]
        public async Task<IActionResult> SecondaryAllocationFileUploadStatus([FromBody] SecondaryAllocationFileUploadStatusParams parameters)
        {
            return await RunQueryPagedServiceAsync<SecondaryAllocationFileUploadStatusParams, SecondaryAllocationFileUploadStatusDto>(parameters, _processAllocationService.SecondaryAllocationFileUploadStatus);
        }
    }
}