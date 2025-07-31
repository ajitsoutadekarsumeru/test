using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost()]
        [Route("treatment/search")]
        [Authorize(Policy = "CanSearchTreatmentPolicy")]
        [ProducesResponseType(typeof(IEnumerable<SearchTreatmentsDto>), 200)]
        public async Task<IActionResult> SearchTreatments([FromBody] SearchTreatmentsParams parameters)
        {
            return await RunQuerySingleServiceAsync<SearchTreatmentsParams, SearchTreatmentsDto>(
                        parameters, _processTreatmentService.SearchTreatments);
        }
    }
}