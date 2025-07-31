using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/add")]
        [Authorize(Policy = "CanCreateTreatmentPolicy")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(string), 201)]
        public async Task<IActionResult> AddTreatment([FromBody] AddTreatmentDto dto)
        {
            var result = RateLimit(dto, "add_treatment");
            return result ?? await RunService(201, dto, _processTreatmentService.AddTreatment);
        }
    }
}