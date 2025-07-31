using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    public partial class AgencyUsersController : FlexControllerBridge<AgencyUsersController>
    {
        [HttpGet()]
        [Route("Search/Agent/byregion/{region}")]
        [ProducesResponseType(typeof(IEnumerable<GetAgentsByRegionDto>), 200)]
        public async Task<IActionResult> GetAgentsByRegion(string region)
        {
            GetAgentsByRegionParams parameters = new GetAgentsByRegionParams()
            {
                Region = region
            };
            return await RunQueryListServiceAsync<GetAgentsByRegionParams, GetAgentsByRegionDto>(parameters, _processAgencyUsersService.GetAgentsByRegion);
        }
    }
}