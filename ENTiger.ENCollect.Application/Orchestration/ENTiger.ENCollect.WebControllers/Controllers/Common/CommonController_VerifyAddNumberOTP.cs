using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/enc/VerifyOTP")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> VerifyAddNumberOTP([FromBody] VerifyAddNumberOTPDto dto)
        {
            return await RunService(201, dto, _processCommonService.VerifyAddNumberOTP);
        }
    }
}