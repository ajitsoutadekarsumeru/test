using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPost]
        [Route("agent/validate")]
        [Authorize(Policy = "CanCreateAgentPolicy")]
        [Authorize(Policy = "CanUpdateAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> ValidateAgent([FromBody] ValidateAgentDto dto)
        {
            return await RunService(201, dto, _processAgencyUsersService.ValidateAgent);
        }
    }
}