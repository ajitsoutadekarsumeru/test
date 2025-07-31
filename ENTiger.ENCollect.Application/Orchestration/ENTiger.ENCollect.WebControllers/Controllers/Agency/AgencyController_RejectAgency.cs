using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPut]
        [Route("reject/collectionagency")]
        [Authorize(Policy = "CanRejectAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> RejectAgency([FromBody] RejectAgencyDto dto)
        {
            var result = RateLimit(dto, "reject_agency");
            return result ?? await RunService(200, dto, _processAgencyService.RejectAgency);
        }
    }
}