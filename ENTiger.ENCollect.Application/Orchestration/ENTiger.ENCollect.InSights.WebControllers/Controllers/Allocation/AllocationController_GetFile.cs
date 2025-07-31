using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class AllocationController : FlexControllerBridge<AllocationController>
    {
        [HttpGet]
        [Route("insight/getfile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> GetFile(string TransactionId)
        {
            GetFileDto dto = new GetFileDto() { TransactionId = TransactionId };
            var result = await RunService(200, dto, _processAllocationService.GetFile);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return _fileTransferUtility.DownloadFileAsZip(dto.FileName,dto.FilePath);
                //return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
            }
            return result;


        }
    }
}