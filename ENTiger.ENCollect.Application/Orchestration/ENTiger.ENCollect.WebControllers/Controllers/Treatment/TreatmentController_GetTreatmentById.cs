using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost()]
        [Route("treatment/view")]
        [ProducesResponseType(typeof(GetTreatmentByIdDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTreatmentById([FromBody] GetTreatmentByIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetTreatmentByIdParams, GetTreatmentByIdDto>(
                        parameters, _processTreatmentService.GetTreatmentById);
        }
    }
}