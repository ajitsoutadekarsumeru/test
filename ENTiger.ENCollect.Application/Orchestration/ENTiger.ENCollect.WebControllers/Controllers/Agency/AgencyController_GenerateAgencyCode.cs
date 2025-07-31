using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpGet()]
        [Route("generateagencycode")]
        [Authorize(Policy = "CanCreateAgencyPolicy")]
        [ProducesResponseType(typeof(GenerateAgencyCodeDto), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GenerateAgencyCode()
        {
            GenerateAgencyCodeParams parameters = new GenerateAgencyCodeParams();
            return await RunQuerySingleServiceAsync<GenerateAgencyCodeParams, GenerateAgencyCodeDto>(parameters, _processAgencyService.GenerateAgencyCode);
        }
    }
}