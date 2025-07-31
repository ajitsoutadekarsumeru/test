using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPut]
        [Route("Agent/Approve")]
        [Authorize(Policy = "CanApproveAgentPolicy")]        
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ApproveAgent([FromBody] ApproveAgentDto dto)
        {
            var result = RateLimit(dto, "approve_agent");
            return result ?? await RunService(200, dto, _processAgencyUsersService.ApproveAgent);
        }
    }
}