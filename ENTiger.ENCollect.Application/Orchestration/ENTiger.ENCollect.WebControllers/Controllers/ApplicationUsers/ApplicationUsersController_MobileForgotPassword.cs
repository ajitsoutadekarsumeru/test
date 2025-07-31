using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/ForgotPassword/Mobile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> MobileForgotPassword([FromBody] MobileForgotPasswordDto dto)
        {
            return await RunService(201, dto, _processApplicationUsersService.MobileForgotPassword);
        }
    }
}