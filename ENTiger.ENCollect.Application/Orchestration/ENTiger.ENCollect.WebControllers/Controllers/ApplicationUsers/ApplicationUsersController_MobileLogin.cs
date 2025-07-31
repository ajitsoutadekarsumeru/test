using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/SendOTP/Login")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> MobileLogin([FromBody] MobileLoginDto dto)
        {           
            return await RunService(201, dto, _processApplicationUsersService.MobileLogin);
        }
    }
}