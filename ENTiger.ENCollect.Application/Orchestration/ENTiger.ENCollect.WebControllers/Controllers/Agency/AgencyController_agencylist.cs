using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("agencylist")]
        [ProducesResponseType(typeof(IEnumerable<agencylistDto>), 200)]
        public async Task<IActionResult> agencylist([FromQuery] agencylistParams parameters)
        {
            return await RunQueryListServiceAsync<agencylistParams, agencylistDto>(
                        parameters, _processAgencyService.agencylist);
        }
    }
}