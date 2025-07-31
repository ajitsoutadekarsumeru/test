using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPut]
        [Route("Agency/Renew")]
        [Authorize(Policy = "CanRenewAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> RenewAgency([FromBody] RenewAgencyDto dto)
        {
            var result = RateLimit(dto, "renew_agency_");
            return result ?? await RunService(200, dto, _processAgencyService.RenewAgency);
        }
    }
}