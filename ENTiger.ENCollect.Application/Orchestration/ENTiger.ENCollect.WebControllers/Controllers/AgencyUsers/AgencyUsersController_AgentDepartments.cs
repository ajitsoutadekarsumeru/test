using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpGet()]
        [Route("agencyuser/department/list")]
        [ProducesResponseType(typeof(AgentDepartmentsDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AgentDepartments()
        {
            AgentDepartmentsParams parameters = new AgentDepartmentsParams();
            return await RunQueryListServiceAsync<AgentDepartmentsParams, AgentDepartmentsDto>(parameters, _processAgencyUsersService.AgentDepartments);
        }
    }
}