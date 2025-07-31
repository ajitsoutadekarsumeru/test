using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CommonController : FlexControllerBridge<CommonController>
    {
        [HttpPost]
        [Route("mvp/enc/sendOTP")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> SendOTPToVerifyNumber([FromBody] SendOTPToVerifyNumberDto dto)
        {
            var result = RateLimit(dto, "send_OTP");
            return result ?? await RunService(201, dto, _processCommonService.SendOTPToVerifyNumber);
        }
    }
}