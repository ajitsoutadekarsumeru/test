using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/ByName")]
        [ProducesResponseType(typeof(IEnumerable<SearchTreatmentsByNameDto>), 200)]
        public async Task<IActionResult> SearchTreatmentsByName([FromBody] SearchTreatmentsByNameParams parameters)
        {
            return await RunQueryListServiceAsync<SearchTreatmentsByNameParams, SearchTreatmentsByNameDto>(
                        parameters, _processTreatmentService.SearchTreatmentsByName);
        }
    }
}