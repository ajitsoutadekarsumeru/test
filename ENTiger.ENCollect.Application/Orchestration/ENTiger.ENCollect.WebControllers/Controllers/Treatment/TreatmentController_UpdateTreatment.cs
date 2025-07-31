using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/edit")]
        [Authorize(Policy = "CanUpdateTreatmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> UpdateTreatment([FromBody] UpdateTreatmentDto dto)
        {
            var result = RateLimit(dto, "update_treatment");
            return result ?? await RunService(200, dto, _processTreatmentService.UpdateTreatment);
        }
    }
}