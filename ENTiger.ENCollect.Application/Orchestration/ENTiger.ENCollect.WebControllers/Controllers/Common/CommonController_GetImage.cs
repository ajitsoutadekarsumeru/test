using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet]
        [Route("mvp/getimage")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetImage(string fileName)
        {
            GetImageDto dto = new GetImageDto() { FileName = fileName };
            var result = await RunService(200, dto, _processCommonService.GetImage);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
            }
            return result;
        }
    }
}