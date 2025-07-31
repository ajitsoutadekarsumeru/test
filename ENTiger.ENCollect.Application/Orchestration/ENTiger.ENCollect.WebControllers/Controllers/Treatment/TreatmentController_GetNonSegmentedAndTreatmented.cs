using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost()]
        [Route("treatment/notsegmentedandtreatmented")]
        [ProducesResponseType(typeof(GetNonSegmentedAndTreatmentedDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetNonSegmentedAndTreatmented([FromBody] GetNonSegmentedAndTreatmentedParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetNonSegmentedAndTreatmentedParams, GetNonSegmentedAndTreatmentedDto>(
                        parameters, _processTreatmentService.GetNonSegmentedAndTreatmented);
        }
    }
}