using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sumeru.Flex;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpPost]
        [Route("search/Agent")]
        [Authorize(Policy = "CanSearchAgentPolicy")]
        [ProducesResponseType(typeof(FlexiPagedList<searchAgentDto>), 200)]
        public async Task<IActionResult> searchAgent([FromBody] searchAgentParams parameters)
        {
            return await RunQueryPagedServiceAsync<searchAgentParams, searchAgentDto>(parameters, _processAgencyUsersService.searchAgent);
        }
    }
}