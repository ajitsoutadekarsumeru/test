using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/clearsegmentandtreatment")]
        [Authorize(Policy = "CanClearSegmentAndTreatementStampingPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ClearSegmentAndTreatment([FromBody] ClearSegmentAndTreatmentDto dto)
        {
            var result = RateLimit(dto, "clear_segmentandtreatment");
            return result ?? await RunService(200, dto, _processTreatmentService.ClearSegmentAndTreatment);
        }
    }
}