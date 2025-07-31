using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpPost]
        [Route("unallocation/getfile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetUnAllocationFile([FromBody]GetUnAllocationFileDto dto)
        {
            var result = await RunService(200, dto, _processAllocationService.GetUnAllocationFile);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName, (result as ObjectResult).Value.ToString());
            }
            return result;
        }
    }
}
