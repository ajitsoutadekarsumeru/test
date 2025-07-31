using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPut]
        [Route("Agent/Renew")]
        [Authorize(Policy = "CanRenewAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> RenewAgent([FromBody] RenewAgentDto dto)
        {
            var result = RateLimit(dto, "renew_agent");
            return result ?? await RunService(200, dto, _processAgencyUsersService.RenewAgent);
        }
    }
}