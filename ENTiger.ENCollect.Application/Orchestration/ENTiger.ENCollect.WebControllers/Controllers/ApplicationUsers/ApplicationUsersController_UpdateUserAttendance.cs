using ENTiger.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [Authorize]
        [ServiceFilter(typeof(CheckUserSessionAccess))]
        [ServiceFilter(typeof(CheckUserLoginActivity))]
        [HttpPost]
        [Route("mvp/edit/userattendance")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateUserAttendance([FromBody] UpdateUserAttendanceDto dto)
        {
            var result = RateLimit(dto, "update_userattendance");
            return result ?? await RunService(200, dto, _processApplicationUsersService.UpdateUserAttendance);
        }
    }
}