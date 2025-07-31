using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/disable")]
        [Authorize(Policy = "CanDisableTreatmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DisableTreatments([FromBody] DisableTreatmentsDto dto)
        {
            var result = RateLimit(dto, "disable_treatment");
            return result ?? await RunService(200, dto, _processTreatmentService.DisableTreatments);
        }
    }
}