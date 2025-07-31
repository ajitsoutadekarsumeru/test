using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpGet()]
        [Route("agents/list")]
        [ProducesResponseType(typeof(IEnumerable<GetAgentsListDto>), 200)]
        public async Task<IActionResult> GetAgentsList([FromQuery] GetAgentsListParams parameters)
        {
            return await RunQueryListServiceAsync<GetAgentsListParams, GetAgentsListDto>(parameters, _processAgencyUsersService.GetAgentsList);
        }
    }
}