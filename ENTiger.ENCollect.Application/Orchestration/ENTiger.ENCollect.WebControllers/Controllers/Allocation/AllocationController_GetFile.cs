using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpGet]
        [Route("Generatedfiledownload")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> GetFile(string fileName)
        {
            GetGeneratedFileDto dto = new GetGeneratedFileDto() { FileName = fileName };
            var result = await RunService(200, dto, _processAllocationService.GetGeneratedFile);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
            }
            return result;
        }
    }
}