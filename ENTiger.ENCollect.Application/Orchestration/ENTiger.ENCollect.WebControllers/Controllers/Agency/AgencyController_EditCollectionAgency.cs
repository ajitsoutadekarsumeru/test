using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPut]
        [Route("edit/collectionagency")]
        [Authorize(Policy = "CanUpdateAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> EditCollectionAgency([FromBody] EditCollectionAgencyDto dto)
        {
            var result = RateLimit(dto, "update_agency");
            return result ?? await RunService(200, dto, _processAgencyService.EditCollectionAgency);
        }
    }
}