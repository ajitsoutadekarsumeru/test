using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpGet()]
        [Route("agencyuser/AgentByagencyId/{Id}")]
        [ProducesResponseType(typeof(IEnumerable<GetAgentsByAgencyIdDto>), 200)]
        public async Task<IActionResult> GetAgentsByAgencyId(string Id)
        {
            GetAgentsByAgencyIdParams parameters = new GetAgentsByAgencyIdParams();
            parameters.Id = Id;
            return await RunQueryListServiceAsync<GetAgentsByAgencyIdParams, GetAgentsByAgencyIdDto>(
                        parameters, _processAgencyUsersService.GetAgentsByAgencyId);
        }
    }
}