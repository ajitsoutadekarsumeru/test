using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ENTiger.ENCollect.SettlementModule
{

    public partial class SettlementController : FlexControllerBridge<SettlementController>
    {
        [HttpGet()]
        [Authorize(Policy = "CanViewMySettlementPolicy")]
        [Route("documents")]
        [ProducesResponseType(typeof(GetDocumentsByIdDto), 200)]
        public async Task<IActionResult> GetDocumentsById([FromQuery]GetDocumentsByIdParams parameters)
        {
            return await RunQuerySingleServiceAsync<GetDocumentsByIdParams, GetDocumentsByIdDto>(parameters, _processSettlementService.GetDocumentsById);
        }
    }
}
