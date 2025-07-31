using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.AgencyModule
{
    public partial class AgencyController : FlexControllerBridge<AgencyController>
    {
        [HttpPut]
        [Route("approvewithdeferral/collectionagency")]
        [Authorize(Policy = "CanApproveAgencyPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ApproveAgencyWithDeferral([FromBody] ApproveAgencyWithDeferralDto dto)
        {
            var result = RateLimit(dto, "approve_deferral_agency");
            return result ?? await RunService(200, dto, _processAgencyService.ApproveAgencyWithDeferral);
        }
    }
}