using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpGet]
        [Route("mvp/getlogo")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> GetLogo()
        {
            GetLogoDto dto = new GetLogoDto();
            var result = await RunService(200, dto, _processCommonService.GetLogo);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync((result as ObjectResult).Value.ToString());
            }
            return result;
        }
    }
}