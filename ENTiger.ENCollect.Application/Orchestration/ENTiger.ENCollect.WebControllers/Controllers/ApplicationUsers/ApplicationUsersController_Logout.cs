using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [Authorize]
        [ServiceFilter(typeof(CheckUserSessionAccess))]
        [HttpPost]
        [Route("Account/Logout")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> Logout([FromBody] LogoutDto dto)
        {
            return await RunService(201, dto, _processApplicationUsersService.Logout);
        }
    }
}