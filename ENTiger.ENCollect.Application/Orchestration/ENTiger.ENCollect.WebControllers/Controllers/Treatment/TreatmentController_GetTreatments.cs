using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpGet()]
        [Route("treatment/alltreatments")]
        [ProducesResponseType(typeof(IEnumerable<GetTreatmentsDto>), 200)]
        public async Task<IActionResult> GetTreatments([FromBody] GetTreatmentsParams parameters)
        {
            return await RunQueryListServiceAsync<GetTreatmentsParams, GetTreatmentsDto>(
                        parameters, _processTreatmentService.GetTreatments);
        }
    }
}