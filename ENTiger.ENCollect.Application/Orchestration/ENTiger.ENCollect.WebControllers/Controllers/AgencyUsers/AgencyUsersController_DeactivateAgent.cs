using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPost]
        [Route("Agent/Deactivate")]
        [Authorize(Policy = "CanDisableAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeactivateAgent([FromBody] DeactivateAgentDto dto)
        {
            var result = RateLimit(dto, "deactivate_agent");
            return result ?? await RunService(200, dto, _processAgencyUsersService.DeactivateAgent);
        }
    }
}