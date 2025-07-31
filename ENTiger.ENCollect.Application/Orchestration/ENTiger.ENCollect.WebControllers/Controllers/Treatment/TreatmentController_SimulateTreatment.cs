using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost()]
        [Route("treatment/simulate")]
        [ProducesResponseType(typeof(IEnumerable<SimulateTreatmentDto>), 200)]
        public async Task<IActionResult> SimulateTreatment([FromBody] SimulateTreatmentParams parameters)
        {
            return await RunQueryListServiceAsync<SimulateTreatmentParams, SimulateTreatmentDto>(
                        parameters, _processTreatmentService.SimulateTreatment);
        }
    }
}