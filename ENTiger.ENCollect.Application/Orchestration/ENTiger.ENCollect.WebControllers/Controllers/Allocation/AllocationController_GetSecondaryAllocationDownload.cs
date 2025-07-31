using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("batch/allocations/secondary/download")]
        [ProducesResponseType(typeof(FlexiPagedList<GetSecondaryAllocationDownloadDto>), 200)]
        public async Task<IActionResult> GetSecondaryAllocationDownload([FromBody] GetSecondaryAllocationDownloadParams parameters)
        {
            parameters.AllocationType = AllocationTypeEnum.Secondary.Value.ToLower();
            return await RunQueryPagedServiceAsync<GetSecondaryAllocationDownloadParams, GetSecondaryAllocationDownloadDto>(parameters, _processAllocationService.GetSecondaryAllocationDownload);
        }
    }
}