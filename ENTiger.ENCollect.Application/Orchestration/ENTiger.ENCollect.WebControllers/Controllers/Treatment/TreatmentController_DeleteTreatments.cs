using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost()]
        [Route("treatment/delete")]
        [Authorize(Policy = "CanDeleteTreatmentPolicy")]
        public async Task<IActionResult> DeleteTreatments([FromBody] DeleteTreatmentsDto dto)
        {
            var result = RateLimit(dto, "delete_treatment");
            return result ?? await RunService(200, dto, _processTreatmentService.DeleteTreatments);
        }
    }
}