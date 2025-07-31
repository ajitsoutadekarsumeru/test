using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("batch/allocations/primary/download")]
        [ProducesResponseType(typeof(FlexiPagedList<GetPrimaryAllocationDownloadDto>), 200)]
        public async Task<IActionResult> GetPrimaryAllocationDownload([FromBody] GetPrimaryAllocationDownloadParams parameters)
        {
            parameters.AllocationType = AllocationTypeEnum.Primary.Value.ToLower();
            return await RunQueryPagedServiceAsync<GetPrimaryAllocationDownloadParams, GetPrimaryAllocationDownloadDto>(parameters, _processAllocationService.GetPrimaryAllocationDownload);
        }
    }
}