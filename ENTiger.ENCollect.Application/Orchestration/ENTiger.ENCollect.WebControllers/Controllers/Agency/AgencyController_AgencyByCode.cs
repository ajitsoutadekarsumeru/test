using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("agency/code")]
        [Authorize(Policy = "CanCreateAgencyPolicy")]
        [ProducesResponseType(typeof(AgencyByCodeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AgencyByCode(string code)
        {
            AgencyByCodeParams parameters = new AgencyByCodeParams();
            parameters.code = code;

            return await RunQuerySingleServiceAsync<AgencyByCodeParams, AgencyByCodeDto>(
                        parameters, _processAgencyService.AgencyByCode);
        }
    }
}