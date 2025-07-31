using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPut]
        [Route("approve/collectionagency")]
        [Authorize(Policy = "CanApproveAgencyPolicy")]
        [Authorize(Policy = "CanEnableAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ApproveCollectionAgency([FromBody] ApproveCollectionAgencyDto dto)
        {
            var result = RateLimit(dto, "approve_agency");
            return result ?? await RunService(200, dto, _processAgencyService.ApproveCollectionAgency);
        }
    }
}