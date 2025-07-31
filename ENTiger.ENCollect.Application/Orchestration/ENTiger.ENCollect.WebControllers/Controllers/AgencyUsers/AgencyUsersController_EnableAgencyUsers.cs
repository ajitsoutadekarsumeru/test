using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPut]
        [Route("agencyusers/enable")]
        [Authorize(Policy = "CanEnableAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> EnableAgencyUsers([FromBody]EnableAgencyUsersDto dto)
        {
            return await RunService(200, dto, _processAgencyUsersService.EnableAgencyUsers);
        }
    }
}
