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
        [Route("mvp/um/getUserTypeDetails")]
        [ProducesResponseType(typeof(GetUserTypeDetailsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserTypeDetails([FromQuery] GetUserTypeDetailsParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetUserTypeDetailsParams, GetUserTypeDetailsDto>(
                        parameters, _processApplicationUsersService.GetUserTypeDetails);
        }
    }
}
