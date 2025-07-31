using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [Authorize]
        [ServiceFilter(typeof(CheckUserSessionAccess))]
        [HttpGet()]
        [Route("mvp/um/loggedinuser/details")]
        [ProducesResponseType(typeof(LoggedInUserDetailsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LoggedInUserDetails([FromQuery] LoggedInUserDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<LoggedInUserDetailsParams, LoggedInUserDetailsDto>(
                        parameters, _processApplicationUsersService.LoggedInUserDetails);
        }
    }
}