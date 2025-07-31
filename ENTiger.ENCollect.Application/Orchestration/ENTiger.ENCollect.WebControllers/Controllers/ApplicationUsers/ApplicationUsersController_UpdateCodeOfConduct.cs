using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class ApplicationUsersController : FlexControllerBridge<ApplicationUsersController>
    {
        [Authorize]
        [ServiceFilter(typeof(CheckUserSessionAccess))]
        [HttpPost]
        [Route("mvp/um/policy/update")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateCodeOfConduct([FromBody]UpdateCodeOfConductDto dto)
        {
            return await RunService(200, dto, _processApplicationUsersService.UpdateCodeOfConduct);
        }
    }
}
