using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpPost]
        [Route("treatment/getexecutionhistorydetails")]
        [ProducesResponseType(typeof(IEnumerable<GetExecutionHistoryDetailsDto>), 200)]
        public async Task<IActionResult> GetExecutionHistoryDetails([FromBody] GetExecutionHistoryDetailsParams parameters)
        {
            return await RunQueryListServiceAsync<GetExecutionHistoryDetailsParams, GetExecutionHistoryDetailsDto>(
                        parameters, _processTreatmentService.GetExecutionHistoryDetails);
        }
    }
}