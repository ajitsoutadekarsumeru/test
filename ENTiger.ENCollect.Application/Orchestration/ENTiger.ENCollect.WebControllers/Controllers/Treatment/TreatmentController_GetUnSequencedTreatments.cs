using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpGet()]
        [Route("Get/unsequencedtreatments")]
        [ProducesResponseType(typeof(IEnumerable<GetUnSequencedTreatmentsDto>), 200)]
        public async Task<IActionResult> GetUnSequencedTreatments()
        {
            GetUnSequencedTreatmentsParams parameters = new GetUnSequencedTreatmentsParams();
            return await RunQueryListServiceAsync<GetUnSequencedTreatmentsParams, GetUnSequencedTreatmentsDto>(
                        parameters, _processTreatmentService.GetUnSequencedTreatments);
        }
    }
}