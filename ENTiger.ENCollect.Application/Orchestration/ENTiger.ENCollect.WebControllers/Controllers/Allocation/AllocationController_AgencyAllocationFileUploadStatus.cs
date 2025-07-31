using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("accounts/allocations/agency/batch/status")]
        [Authorize(Policy = "CanSearchPrimaryAllocationBatchStatusPolicy")]
        [Authorize(Policy = "CanSearchAllocationOwnerBatchStatusPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<AgencyAllocationFileUploadStatusDto>), 200)]
        public async Task<IActionResult> AgencyAllocationFileUploadStatus([FromBody] AgencyAllocationFileUploadStatusParams parameters)
        {
            return await RunQueryPagedServiceAsync<AgencyAllocationFileUploadStatusParams, AgencyAllocationFileUploadStatusDto>(parameters, _processAllocationService.AgencyAllocationFileUploadStatus);
        }
    }
}