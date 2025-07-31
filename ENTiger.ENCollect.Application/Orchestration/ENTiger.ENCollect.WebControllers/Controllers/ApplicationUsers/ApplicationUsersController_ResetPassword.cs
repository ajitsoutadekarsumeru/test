using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/ResetPassword")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            return await RunService(201, dto, _processApplicationUsersService.ResetPassword);
        }
    }
}