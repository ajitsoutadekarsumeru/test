using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class TreatmentController : FlexControllerBridge<TreatmentController>
    {
        [HttpGet()]
        [Route("Get/account/detail")]
        [ProducesResponseType(typeof(GetTreatmentAccountsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTreatmentAccounts(string treatmenthistoryId)
        {
            GetTreatmentAccountsParams parameters = new GetTreatmentAccountsParams();
            parameters.Id = treatmenthistoryId;

            return await RunQuerySingleServiceAsync<GetTreatmentAccountsParams, GetTreatmentAccountsDto>(
                        parameters, _processTreatmentService.GetTreatmentAccounts);
        }
    }
}