using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/ResetPassword/Mobile")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> MobileResetPassword([FromBody] MobileResetPasswordDto dto)
        {
            return await RunService(201, dto, _processApplicationUsersService.MobileResetPassword);
        }
    }
}