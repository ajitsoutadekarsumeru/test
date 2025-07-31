using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPut]
        [Route("deactivate/collectionagency")]
        [Authorize(Policy = "CanDisableAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeactivateAgency([FromBody] DeactivateAgencyDto dto)
        {
            var result = RateLimit(dto, "deactivate_agency");
            return result ?? await RunService(200, dto, _processAgencyService.DeactivateAgency);
        }
    }
}