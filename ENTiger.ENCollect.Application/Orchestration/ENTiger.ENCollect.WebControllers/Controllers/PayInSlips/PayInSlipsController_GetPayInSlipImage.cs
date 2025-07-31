using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class PayInSlipsController : FlexControllerBridge<PayInSlipsController>
    {
        [HttpGet]
        [Route("payinslip/getimage")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> GetPayInSlipImage(string fileName)
        {
            GetPayInSlipImageDto dto = new GetPayInSlipImageDto() { FileName = fileName };
            var result = await RunService(200, dto, _processPayInSlipsService.GetPayInSlipImage);
            if (result is ObjectResult objectResult && objectResult.StatusCode == 200)
            {
                return await _fileTransferUtility.DownloadFileAsync(dto.FileName);
            }
            return result;
        }
    }
}