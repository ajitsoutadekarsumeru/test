using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpGet()]
        [Route("treatment/get/sequencedtreatments")]
        [ProducesResponseType(typeof(IEnumerable<GetSequencedTreatmentsDto>), 200)]
        public async Task<IActionResult> GetSequencedTreatments()
        {
            GetSequencedTreatmentsParams parameters = new GetSequencedTreatmentsParams();
            return await RunQueryListServiceAsync<GetSequencedTreatmentsParams, GetSequencedTreatmentsDto>(parameters, _processTreatmentService.GetSequencedTreatments);
        }
    }
}