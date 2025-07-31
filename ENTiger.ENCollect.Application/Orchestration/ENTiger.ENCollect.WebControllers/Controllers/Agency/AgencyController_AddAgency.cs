using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPost]
        [Route("add/collectionagency")]
        [Authorize(Policy = "CanCreateAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddAgency([FromBody] AddAgencyDto dto)
        {
            var result = RateLimit(dto, "add_agency");
            return result ?? await RunService(201, dto, _processAgencyService.AddAgency);
        }
    }
}