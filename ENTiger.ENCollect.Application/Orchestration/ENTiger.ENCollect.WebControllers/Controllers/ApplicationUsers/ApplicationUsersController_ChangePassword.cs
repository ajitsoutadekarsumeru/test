using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [HttpPost]
        [Route("Account/ChangePassword")]
        [Authorize(Policy = "CanChangePasswordPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            return await RunService(201, dto, _processApplicationUsersService.ChangePassword);
        }
    }
}