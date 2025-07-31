using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPost]
        [Route("add/agent")]
        [Authorize(Policy = "CanCreateAgentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddAgent([FromBody] AddAgentDto dto)
        {
            var result = RateLimit(dto, "add_agent");
            return result ?? await RunService(201, dto, _processAgencyUsersService.AddAgent);
        }
    }
}