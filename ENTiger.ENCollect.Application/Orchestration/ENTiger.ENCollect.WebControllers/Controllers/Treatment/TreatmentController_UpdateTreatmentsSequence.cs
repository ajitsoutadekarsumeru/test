using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/addSequence")]
        [Authorize(Policy = "CanSequenceTreatmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateTreatmentsSequence([FromBody] UpdateTreatmentsSequenceDto dto)
        {
            var result = RateLimit(dto, "update_treatment_sequence");
            return result ?? await RunService(200, dto, _processTreatmentService.UpdateTreatmentsSequence);
        }
    }
}