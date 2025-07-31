using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/enable")]
        [Authorize(Policy = "CanEnableTreatmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> EnableTreatments([FromBody] EnableTreatmentsDto dto)
        {
            var result = RateLimit(dto, "enable_treatment");
            return result ?? await RunService(200, dto, _processTreatmentService.EnableTreatments);
        }
    }
}