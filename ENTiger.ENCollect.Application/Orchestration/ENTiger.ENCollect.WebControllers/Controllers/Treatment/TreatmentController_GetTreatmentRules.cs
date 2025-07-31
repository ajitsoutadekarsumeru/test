using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpGet()]
        [Route("Get/TreatmentRuleName")]
        [ProducesResponseType(typeof(IEnumerable<GetTreatmentRulesDto>), 200)]
        public async Task<IActionResult> GetTreatmentRules()
        {
            GetTreatmentRulesParams parameters = new GetTreatmentRulesParams();
            return await RunQueryListServiceAsync<GetTreatmentRulesParams, GetTreatmentRulesDto>(
                        parameters, _processTreatmentService.GetTreatmentRules);
        }
    }
}