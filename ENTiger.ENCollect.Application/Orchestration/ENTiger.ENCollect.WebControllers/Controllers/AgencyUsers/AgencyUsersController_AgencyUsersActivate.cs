using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPut]
        [Route("agencyuser/activate")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> AgencyUsersActivate([FromBody]AgencyUsersActivateDto dto)
        {
            return await RunService(200, dto, _processAgencyUsersService.AgencyUsersActivate);
        }
    }
}
