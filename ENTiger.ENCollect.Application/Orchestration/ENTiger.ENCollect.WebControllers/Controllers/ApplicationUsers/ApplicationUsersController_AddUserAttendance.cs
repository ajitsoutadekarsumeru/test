using ENTiger.Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [ServiceFilter(typeof(CheckUserLoginActivity))]
        [Authorize]
        [HttpPost]
        [Route("mvp/add/userattendance")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddUserAttendance([FromBody] AddUserAttendanceDto dto)
        {
            var result = RateLimit(dto, "add_userattendance");
            return result ?? await RunService(201, dto, _processApplicationUsersService.AddUserAttendance);
        }
    }
}