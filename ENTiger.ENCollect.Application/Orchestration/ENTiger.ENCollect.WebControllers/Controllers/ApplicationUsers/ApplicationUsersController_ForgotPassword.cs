using ENTiger.ENCollect.CommunicationModule;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/ForgotPassword")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            var result = RateLimit(dto, "forgot_password");
            return result ?? await RunService(201, dto, _processApplicationUsersService.ForgotPassword);
        }
    }
}