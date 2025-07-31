using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPut]
        [Route("Edit/agent")]
        [Authorize(Policy = "CanUpdateAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateAgent([FromBody] UpdateAgentDto dto)
        {
            var result = RateLimit(dto, "update_agent");
            return result ?? await RunService(200, dto, _processAgencyUsersService.UpdateAgent);
        }
    }
}