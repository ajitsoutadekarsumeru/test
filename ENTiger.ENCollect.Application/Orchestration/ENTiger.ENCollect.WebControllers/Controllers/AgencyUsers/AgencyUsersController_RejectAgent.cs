using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPut]
        [Route("Agent/Reject")]
        [Authorize(Policy = "CanRejectAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> RejectAgent([FromBody] RejectAgentDto dto)
        {
            var result = RateLimit(dto, "reject_agent");
            return result ?? await RunService(200, dto, _processAgencyUsersService.RejectAgent);
        }
    }
}