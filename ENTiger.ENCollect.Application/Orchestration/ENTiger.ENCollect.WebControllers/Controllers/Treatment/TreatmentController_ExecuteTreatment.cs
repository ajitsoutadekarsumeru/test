using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/execute")]
        [Authorize(Policy = "CanExecuteTreatmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> ExecuteTreatment([FromBody] ExecuteTreatmentDto dto)
        {
            var result = RateLimit(dto, "execute_treatment");
            return result ?? await RunService(200, dto, _processTreatmentService.ExecuteTreatment);
        }
    }
}