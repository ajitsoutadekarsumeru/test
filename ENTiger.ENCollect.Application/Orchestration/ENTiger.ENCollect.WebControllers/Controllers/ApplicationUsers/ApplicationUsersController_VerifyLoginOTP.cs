using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/VerifyOTP/Login")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> VerifyLoginOTP([FromBody] VerifyLoginOTPDto dto)
        {
            return await RunService(201, dto, _processApplicationUsersService.VerifyLoginOTP);
        }
    }
}