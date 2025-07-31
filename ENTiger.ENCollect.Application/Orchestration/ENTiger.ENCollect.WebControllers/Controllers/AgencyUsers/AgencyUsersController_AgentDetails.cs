using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpGet()]
        [Route("View/Agent/{id}")]
        [Authorize(Policy = "CanViewAgentPolicy")]
        [ProducesResponseType(typeof(AgentDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AgentDetails(string id)
        {
            AgentDetailsParams parameters = new AgentDetailsParams();
            parameters.Id = id;

            return await RunQuerySingleServiceAsync<AgentDetailsParams, AgentDetailsDto>(parameters, _processAgencyUsersService.AgentDetails);
        }
    }
}